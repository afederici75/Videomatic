using Company.Videomatic.Domain.Abstractions;

namespace Infrastructure.Data.Tests;

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
        var createCommand = CreateVideoCommandBuilder.WithRandomValuesAndEmptyVideoDetails();

        CreatedResponse response = await Sender.Send(createCommand);

        // Checks
        response.Id.Should().BeGreaterThan(0);

        // Just a small test to see if LINQ creates a simpler query than
        // the one with all owned properties just down below.
        //var tmp = await Fixture.DbContext.Videos.Where(x => x.Id == response.Id).Select(x=> x.Id).SingleAsync();
        var video = Fixture.DbContext.Videos.Single(x => x.Id == response.Id);
        
        video.Name.Should().BeEquivalentTo(createCommand.Name);
        video.Description.Should().BeEquivalentTo(createCommand.Description);
        video.Location.Should().BeEquivalentTo(createCommand.Location);
        video.Details.ChannelId.Should().BeEquivalentTo(createCommand.ChannelId); 
        video.Details.PlaylistId.Should().BeEquivalentTo(createCommand.PlaylistId);
        video.Details.Provider.Should().BeEquivalentTo(createCommand.Provider);
        video.Details.VideoOwnerChannelId.Should().BeEquivalentTo(createCommand.VideoOwnerChannelId);
        video.Details.VideoOwnerChannelTitle.Should().BeEquivalentTo(createCommand.VideoOwnerChannelTitle);
        video.Details.VideoPublishedAt.Should().Be(createCommand.VideoPublishedAt);        
    }

    [Fact]
    public async Task DeleteVideo()
    {
        var createCommand = CreateVideoCommandBuilder.WithRandomValuesAndEmptyVideoDetails();

        CreatedResponse response = await Sender.Send(createCommand);

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
        CreatedResponse createResponse = await Sender.Send(
            CreateVideoCommandBuilder.WithRandomValuesAndEmptyVideoDetails());

        var updateCommand = new UpdateVideoCommand(
            createResponse.Id,
            "New Title",
            "New Description");

        UpdatedResponse response = await Sender.Send(updateCommand);

        // Checks
        var video = await Fixture.DbContext.Videos
            .Where(x => x.Id == createResponse.Id)
            .SingleAsync();

        video.Id.Value.Should().Be(updateCommand.Id);
        video.Name.Should().BeEquivalentTo(updateCommand.Name);
        video.Description.Should().BeEquivalentTo(updateCommand.Description);        
    }

    [Fact]
    public async Task LinksTwoVideoToPlaylist()
    {
        // Prepares
        var createPlaylistCmd = new CreatePlaylistCommand(
            Name: nameof(LinksTwoVideoToPlaylist),
            Description: $"A description for my playlist {DateTime.Now}");

        CreatedResponse createPlaylistResponse = await Sender.Send(createPlaylistCmd);

        var createVid1Cmd = CreateVideoCommandBuilder
            .WithRandomValuesAndEmptyVideoDetails(nameof(LinksTwoVideoToPlaylist) + "V1");

        CreatedResponse createVid1Response = await Sender.Send(createVid1Cmd);

        var createVid2Cmd = CreateVideoCommandBuilder
            .WithRandomValuesAndEmptyVideoDetails(nameof(LinksTwoVideoToPlaylist) + "V2");

        // Executes
        var addVidsCmd = new LinkVideoToPlaylistsCommand(
            createVid1Response.Id,
            new[] { createPlaylistResponse.Id });

        LinkVideoToPlaylistsResponse addVidsResponse = await Sender.Send(addVidsCmd); // Should add 2 videos
        LinkVideoToPlaylistsResponse emptyAddVidsResponse = await Sender.Send(addVidsCmd); // Should not add anything as they are both dups

        // Checks
        createPlaylistResponse.Id.Should().BeGreaterThan(0);
        createVid1Response.Id.Should().BeGreaterThan(0);
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
