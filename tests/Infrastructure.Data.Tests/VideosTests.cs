﻿namespace Infrastructure.Data.Tests;

[Collection("DbContextTests")]
public class VideosTests : IClassFixture<DbContextFixture>
{
    public VideosTests(
        DbContextFixture fixture,
        ISender sender)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));

        Fixture.SkipDeletingDatabase = true;
    }

    public DbContextFixture Fixture { get; }
    public ISender Sender { get; }

    [Fact]
    public async Task CreateVideo()
    {
        var command = CreateVideoCommandBuilder
            .WithRandomValuesAndEmptyVideoDetails();

        CreatedResponse response = await Sender.Send(command);

        // Checks
        response.Id.Should().BeGreaterThan(0);

        var video = Fixture.DbContext.Videos.Single(x => x.Id == response.Id);

        video.Name.Should().BeEquivalentTo(command.Name);
        video.Description.Should().BeEquivalentTo(command.Description);
        video.Location.Should().BeEquivalentTo(command.Location);
        video.Details.ChannelId.Should().BeEquivalentTo(command.ChannelId); 
        video.Details.PlaylistId.Should().BeEquivalentTo(command.PlaylistId);
        video.Details.Provider.Should().BeEquivalentTo(command.Provider);
        video.Details.VideoOwnerChannelId.Should().BeEquivalentTo(command.VideoOwnerChannelId);
        video.Details.VideoOwnerChannelTitle.Should().BeEquivalentTo(command.VideoOwnerChannelTitle);
        video.Details.VideoPublishedAt.Should().Be(command.VideoPublishedAt);        
    }

    [Fact]
    public async Task DeleteVideo()
    {
        var command = CreateVideoCommandBuilder
            .WithRandomValuesAndEmptyVideoDetails();

        CreatedResponse response = await Sender.Send(command);

        // Checks
        response.Id.Should().BeGreaterThan(0);

        var cnt = await Fixture.DbContext.Videos
            .Where(x => x.Id == response.Id)
            .ExecuteDeleteAsync();

        cnt.Should().Be(1);
    }

    [Fact]
    public async Task UpdateVideo()
    {
        var command = CreateVideoCommandBuilder
            .WithRandomValuesAndEmptyVideoDetails();

        CreatedResponse response = await Sender.Send(command);

        var updateCommand = new UpdateVideoCommand(
            response.Id,
            "New Title",
            "New Description");

        UpdatedResponse updatedResponse = await Sender.Send(updateCommand);

        // Checks
        var video = await Fixture.DbContext.Videos
            .Where(x => x.Id == response.Id)
            .SingleAsync();

        video.Id.Value.Should().Be(updateCommand.Id);
        video.Name.Should().BeEquivalentTo(updateCommand.Name);
        video.Description.Should().BeEquivalentTo(updateCommand.Description);        
    }

    [Fact]
    public async Task LinksOnePlaylistWithTwoVideos()
    {
        // Prepares
        var createPlaylistCmd = new CreatePlaylistCommand(
            Name: nameof(LinksOnePlaylistWithTwoVideos),
            Description: $"A description for my playlist {DateTime.Now}");

        CreatedResponse createPlaylistResponse = await Sender.Send(createPlaylistCmd);

        var createVid1Cmd = CreateVideoCommandBuilder
            .WithRandomValuesAndEmptyVideoDetails(nameof(LinksOnePlaylistWithTwoVideos) + "V1");

        CreatedResponse createVid1Response = await Sender.Send(createVid1Cmd);

        var createVid2Cmd = CreateVideoCommandBuilder
            .WithRandomValuesAndEmptyVideoDetails(nameof(LinksOnePlaylistWithTwoVideos) + "V2");

        CreatedResponse createVid2Response = await Sender.Send(createVid2Cmd);

        // Executes
        var addVidsCmd = new LinkVideosToPlaylistCommand(
            createPlaylistResponse.Id,
            VideoIds: new[] { createVid1Response.Id, createVid2Response.Id });

        LinkVideosToPlaylistResponse addVidsResponse = await Sender.Send(addVidsCmd); // Should add 2 videos
        LinkVideosToPlaylistResponse emptyAddVidsResponse = await Sender.Send(addVidsCmd); // Should not add anything as they are both dups

        // Checks
        createPlaylistResponse.Id.Should().BeGreaterThan(0);
        createVid1Response.Id.Should().BeGreaterThan(0);
        createVid2Response.Id.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(null, null, null, null, true, null, 2)]
    [InlineData(new long[] { 1 }, null, null, null, true, null, 2)]
    [InlineData(new long[] { 1, 2 }, null, null, null, true, null, 2)]
    [InlineData(null, new long[] { 2 }, null, null, true, null, 1)] // By VideoIds
    // TODO: missing paging tests and should add more anyway
    public async Task GetVideos(
        long[]? playlistIds,
        long[]? videoIds,
        string? searchText,
        string? orderBy,
        bool includeCounts,
        ThumbnailResolutionDTO? IncludeThumbnail,
        int expectedResults)
    {
        var query = new GetVideosQuery(
            PlaylistIds: playlistIds, 
            VideoIds: videoIds, 
            SearchText: searchText, 
            OrderBy: orderBy, 
            Page: null, // Uses 1 by default
            PageSize: null, // Uses 10 by default
            IncludeCounts: includeCounts, 
            IncludeThumbnail: IncludeThumbnail);

        PageResult<VideoDTO> response = await Sender.Send(query);

        // Checks
        response.Count.Should().Be(expectedResults);
        response.TotalCount.Should().Be(expectedResults);
    }
}
