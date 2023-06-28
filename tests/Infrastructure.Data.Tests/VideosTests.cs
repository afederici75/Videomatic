using Azure;

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
        var command = new CreateVideoCommand(
            Location: "youtube.com/v?V1", 
            Title: nameof(CreateVideo), 
            Description: "A description");

        CreatedResponse response = await Sender.Send(command);

        // Checks
        response.Id.Should().BeGreaterThan(0);

        var video = Fixture.DbContext.Videos.Single(x => x.Id == response.Id);

        video.Should().BeEquivalentTo(command);
    }

    [Fact]
    public async Task DeleteVideo()
    {
        var command = new CreateVideoCommand(
            Location: "youtube.com/v?V1", 
            Title: nameof(DeleteVideo), 
            Description: "A description");

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
        var command = new CreateVideoCommand(
            Location: "youtube.com/v?V1",
            Title: nameof(UpdateVideo),
            Description: "A description");

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

        video.Should().BeEquivalentTo(updateCommand);
    }

    [Fact]
    public async Task LinksOnePlaylistWithTwoVideos()
    {
        // Prepares
        var createPlaylistCmd = new CreatePlaylistCommand(
            Name: nameof(LinksOnePlaylistWithTwoVideos),
            Description: $"A description for my playlist {DateTime.Now}");

        CreatedResponse createPlaylistResponse = await Sender.Send(createPlaylistCmd);

        var createVid1Cmd = new CreateVideoCommand(
            Location: "youtube.com/v?V1",
            Title: nameof(LinksOnePlaylistWithTwoVideos) + "_1",
            Description: "A description");

        CreatedResponse createVid1Response = await Sender.Send(createVid1Cmd);

        var createVid2Cmd = new CreateVideoCommand(
            Location: "youtube.com/v?V2",
            Title: nameof(LinksOnePlaylistWithTwoVideos) + "_2",
            Description: "A second description");

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
