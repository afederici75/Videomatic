using Newtonsoft.Json;

namespace Infrastructure.Tests.Data;

[Collection("DbContextTests")]
public class VideosTests : IClassFixture<DbContextFixture>
{
    public VideosTests(
        DbContextFixture fixture,
        ISender sender,
        IRepository<Video> repository)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));

        //Fixture.SkipDeletingDatabase = true;
    }

    public DbContextFixture Fixture { get; }
    public ISender Sender { get; }
    public IRepository<Video> Repository { get; }


    //[Fact]
    //public async Task CreateVideo()
    //{
    //    var createCommand = CreateVideoCommandBuilder.WithRandomValuesAndEmptyVideoDetails();
    //
    //    var response = await Sender.Send(createCommand);
    //
    //    // Checks
    //    response.Value.Id.Value.Should().BeGreaterThan(0);
    //
    //    // Just a small test to see if LINQ creates a simpler query than
    //    // the one with all owned properties just down below.
    //    //var tmp = await Fixture.DbContext.Videos.Where(x => x.Id == response.Id).Select(x=> x.Id).SingleAsync();
    //    var video = Fixture.DbContext.Videos.Single(x => x.Id == response.Value.Id);
    //    
    //    video.Name.Should().BeEquivalentTo(createCommand.Name);
    //    video.Description.Should().BeEquivalentTo(createCommand.Description);
    //    //video.Location.Should().BeEquivalentTo(createCommand.Location);
    //    //video.Details.ChannelId.Should().BeEquivalentTo(createCommand.ChannelId); 
    //    //video.Details.PlaylistId.Should().BeEquivalentTo(createCommand.PlaylistId);
    //    video.Origin.ProviderId.Should().BeEquivalentTo(createCommand.ProviderId);
    //    video.Origin.ChannelId.Should().BeEquivalentTo(createCommand.ChannelId);
    //    video.Origin.ChannelName.Should().BeEquivalentTo(createCommand.ChannelName);
    //    video.Origin.PublishedOn.Should().Be(createCommand.PublishedOn);        
    //
    //    Fixture.DbContext.Videos.Remove(video);
    //    await Fixture.DbContext.SaveChangesAsync();
    //}

    [Fact]
    public async Task DeleteVideo()
    {
        Video newVideo = await Repository.AddAsync(new Video(nameof(DeleteVideo)));

        // Executes
        Result deletedResponse = await Sender.Send(new DeleteVideoCommand(newVideo.Id));

        // Checks
        deletedResponse.IsSuccess.Should().BeTrue();
        
        var row = await Fixture.DbContext.Videos.FirstOrDefaultAsync(x => x.Id == newVideo.Id);
        row.Should().BeNull();
    }

    [Fact]
    public async Task UpdateVideo()
    {
        Video newVideo = await Repository.AddAsync(new Video(nameof(DeleteVideo)));
        
        var updateCommand = new UpdateVideoCommand(
            newVideo.Id,
            "New Title",
            "New Description");

        Result<Video> response = await Sender.Send(updateCommand);

        // Checks
        var video = await Fixture.DbContext.Videos
            .Where(x => x.Id == newVideo.Id)
            .SingleAsync();

        video.Id.Value.Should().Be(updateCommand.Id);
        video.Name.Should().BeEquivalentTo(updateCommand.Name);
        video.Description.Should().BeEquivalentTo(updateCommand.Description);

        await Sender.Send(new DeleteVideoCommand(video.Id));
    }

    [Fact]
    public async Task LinksTwoVideoToPlaylist()
    {
        // Playlist
        var createPlaylistCmd = new CreatePlaylistCommand(
            name: nameof(LinksTwoVideoToPlaylist),
            description: $"A description for my playlist {DateTime.Now}");
        
        Result<Playlist> playlist1 = await Sender.Send(createPlaylistCmd);

        // Video 1
        Video video1 = await Repository.AddAsync(new Video(nameof(LinksTwoVideoToPlaylist) + "V1"));

        // Video 2
        Video video2 = await Repository.AddAsync(new Video(nameof(LinksTwoVideoToPlaylist) + "V2"));

        // Links
        var addVidsCmd = new LinkPlaylistToVideosCommand(
            playlist1.Value.Id,
            new VideoId[] { video1.Id, video2.Id });
        
        var addVidsResponse = await Sender.Send(addVidsCmd); // Should add 2 videos
        var emptyAddVidsResponse = await Sender.Send(addVidsCmd); // Should not add anything as they are both dups
        
        // Checks
        addVidsResponse.Value.Should().Be(2);
        emptyAddVidsResponse.Value.Should().Be(0);

        await Sender.Send(new DeletePlaylistCommand(playlist1.Value.Id));
        await Sender.Send(new DeleteVideoCommand(video1.Id));
        await Sender.Send(new DeleteVideoCommand(video2.Id));
    }

    [Theory]
    [InlineData(null, null, null, true, 2)]
    [InlineData(new int[] { 1 }, null, null, true, 2)]
    [InlineData(new int[] { 1, 2 }, null, null, true, 2)]
    [InlineData(new int[] { 1, 2 }, "gods", null, true, 1)]
    public async Task GetVideos(
        int[]? playlistIds,
        string? searchText,
        string? orderBy,
        bool includeTags,
        int expectedResults)
    {


        var query = new GetVideosQuery(
            //VideoIds: videoIds, 
            searchText: searchText,
            orderBy: orderBy,
            skip: null,
            take: null, // Uses 1 by default
            playlistIds: playlistIds?.Select(x => (PlaylistId)x));

        if (searchText != null)
        { }
        Page<VideoDTO> response = await Sender.Send(query);
        if (response.Count != expectedResults)
        { 
            
        }

        // Checks
        response.Count.Should().Be(expectedResults);
        response.TotalCount.Should().Be(expectedResults);
        if (includeTags)
        {            
        }
    }

    [Theory]
    [InlineData(new int[] { 1 }, 1)]
    [InlineData(new int[] { 1, 2 }, 2)]
    [InlineData(new int[] { 2 }, 1)]
    [InlineData(new int[] { 3 }, 0)]
    // TODO: missing paging tests and should add more anyway
    public async Task GetVideosById(
        int[] videoIds,        
        int expectedResults)
    {
        var query = new GetVideosQuery(
            videoIds: videoIds.Select(x => (VideoId)x));

        Page<VideoDTO> res = await Sender.Send(query);

        // Checks
        res.Items.Should().HaveCount(expectedResults);        
    }
}
