namespace Company.Videomatic.Infrastructure.Data.Tests.SqlServer;

[Collection("DbContextTests")]
public class SqlServerDbContextTests : IClassFixture<SqlServerDbContextFixture>
{
    public SqlServerDbContextTests(SqlServerDbContextFixture fixture)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Fixture.SkipDeletingDatabase = true;
    }

    public SqlServerDbContextFixture Fixture { get; }

    [Fact]
    public async Task T01_CreatesEmptyPlaylist()
    {
        // Prepares
        var newPlaylist = new Playlist("My playlist 1", $"A description for my playlist {DateTime.Now}");        

        // Executes
        var updatedPlaylist = await Fixture.PlaylistRepository.CreateAsync(newPlaylist);
        
        // Asserts
        newPlaylist.Id.Should().Be(0);
        updatedPlaylist.Id.Should().BeGreaterThan(0);

        var fromDbPlaylist = await Fixture.PlaylistRepository.GetByIdAsync(new (updatedPlaylist.Id));
        fromDbPlaylist.Should().NotBeNull();
        fromDbPlaylist.Should().BeEquivalentTo(updatedPlaylist);        
    }

    [Fact]
    public async Task T02_CreatesPlaylistWithTwoVideos()
    {
        // Prepares
        var newPlaylist = new Playlist(name: "My playlist 2", description: $"A playlist with 2 videos {DateTime.Now}");
        var vid1 = new Video(location: "youtube.com/v?V1", title: "A title", description: "A description");        
        var vid2 = new Video(location: "youtube.com/v?V2", title: "A second title", description: "A second description");

        newPlaylist.AddVideo(vid1)
                   .AddVideo(vid2);

        // Executes
        var updatedPlaylist = await Fixture.PlaylistRepository.CreateAsync(newPlaylist);

        // Asserts
        updatedPlaylist.Id.Should().BeGreaterThan(0);

        var fromDbPlaylist = await Fixture.PlaylistRepository.GetByIdAsync(new (updatedPlaylist.Id));
        fromDbPlaylist.Should().NotBeNull();
        fromDbPlaylist!.Videos.Should().HaveCount(0); // We did not specify any includes        
    }

    readonly string[] AllPlaylistFields = new[] { nameof(Playlist.Videos), "Videos.Thumbnails", "Videos.Tags", "Videos.Artifacts", "Videos.Transcripts", "Videos.Transcripts.Lines" };

    [Fact]
    public async Task T03_CreatePlaylistWithACompleteVideo()
    {
        // Prepares
        var newPlaylist = new Playlist(name: "My playlist 3", description: $"A playlist with 2 complete videos {DateTime.Now}");
        var vid1 = new Video(location: "youtube.com/v?VCompleteA", title: "A complete title", description: "A complete description");
        
        vid1.AddThumbnail(new Thumbnail(location: "youtubethumbs.com/T1_1", resolution: ThumbnailResolution.Default, height: 100, width: 100))
            .AddThumbnail(new Thumbnail(location: "youtubethumbs.com/T1_2", resolution: ThumbnailResolution.Medium, height: 200, width: 200));

        vid1.AddTag(new Tag("Tag1"))
            .AddTag(new Tag("Tag2"));

        vid1.AddArtifact(new Artifact(title: "A complete summary", type: "AI", text: "Bla bla"))
            .AddArtifact(new Artifact(title: "A complete analysis", type: "AI", text: "More bla bla"));
        
        var trans1 = new Transcript("EN");
        trans1.AddLine(new TranscriptLine(text: "This is", startsAt: TimeSpan.FromSeconds(0), duration: TimeSpan.FromSeconds(1)));
        trans1.AddLine(new TranscriptLine(text: "a long transcript", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2)));

        var trans2 = new Transcript("IT");
        trans1.AddLine(new TranscriptLine(text: "Questa e'", startsAt: TimeSpan.FromSeconds(1), duration: TimeSpan.FromSeconds(1)));
        trans1.AddLine(new TranscriptLine(text: "una lunga transcrizione", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2)));

        vid1.AddTranscript(trans1)
            .AddTranscript(trans2);
        
        newPlaylist.AddVideo(vid1);

        // Executes
        var updatedPlaylist = await Fixture.PlaylistRepository.CreateAsync(newPlaylist);

        // Asserts
        newPlaylist.Id.Should().Be(0);
        updatedPlaylist.Id.Should().BeGreaterThan(0);

        var fromDbPlaylist = await Fixture.PlaylistRepository.GetByIdAsync(new(updatedPlaylist.Id, AllPlaylistFields));

        fromDbPlaylist.Should().NotBeNull();
        fromDbPlaylist!.Videos.Should().HaveCount(newPlaylist.Videos.Count);
        foreach (var vid in fromDbPlaylist!.Videos)
        {
            vid.Thumbnails.Should().NotBeEmpty();
            vid.Tags.Should().NotBeEmpty();
            vid.Artifacts.Should().NotBeEmpty();            
            vid.Transcripts.Should().NotBeEmpty();
            vid.Transcripts.First().Lines.Should().NotBeEmpty();    
        }

        // If I use Should().BeEquivalentTo I get the following error. I assume it is because of Video.Playlists...
        //    Expected fromDbPlaylist2.Videos[0].Tags[0].Videos[0] to be [youtube.com/v?VCompleteA, Thumbnails: 2, Transcripts: 2] A complete title, but it contains a cyclic reference.
        //    Expected fromDbPlaylist2.Videos[0].Tags[1].Videos[0] to be[youtube.com / v ? VCompleteA, Thumbnails: 2, Transcripts: 2] A complete title, but it contains a cyclic reference.
        //    Expected fromDbPlaylist2.Videos[0].Playlists[0] to be Company.Videomatic.Domain.Model.Playlist
        //    { JSON }, but it contains a cyclic reference.
        //fromDbPlaylist2.Should().BeEquivalentTo(updatedPlaylist);
    }

    [Fact]
    public async Task T04_CreateNonEmptyPlaylistAndUpdatesIt()
    {
        // Prepares
        var newPlaylist = new Playlist(name: "My playlist 4", description: $"A playlist with 2 complete videos {DateTime.Now}");
        var vid1 = new Video(location: "youtube.com/v?VCompleteA", title: "A complete title", description: "A complete description");

        vid1.AddThumbnail(new Thumbnail(location: "youtubethumbs.com/T1_1", resolution: ThumbnailResolution.Default, height: 100, width: 100))
            .AddThumbnail(new Thumbnail(location: "youtubethumbs.com/T1_2", resolution: ThumbnailResolution.Medium, height: 200, width: 200));

        vid1.AddTag(new Tag("Tag1"))
            .AddTag(new Tag("Tag2"));

        vid1.AddArtifact(new Artifact(title: "A complete summary", type: "AI", text: "Bla bla"))
            .AddArtifact(new Artifact(title: "A complete analysis", type: "AI", text: "More bla bla"));

        var trans1 = new Transcript("EN");
        trans1.AddLine(new TranscriptLine(text: "This is", startsAt: TimeSpan.FromSeconds(0), duration: TimeSpan.FromSeconds(1)));
        trans1.AddLine(new TranscriptLine(text: "a long transcript", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2)));

        var trans2 = new Transcript("IT");
        trans1.AddLine(new TranscriptLine(text: "Questa e'", startsAt: TimeSpan.FromSeconds(1), duration: TimeSpan.FromSeconds(1)));
        trans1.AddLine(new TranscriptLine(text: "una lunga transcrizione", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2)));

        vid1.AddTranscript(trans1)
            .AddTranscript(trans2);

        newPlaylist.AddVideo(vid1);

        // Executes
        var updatedPlaylist = await Fixture.PlaylistRepository.CreateAsync(newPlaylist);
        var fromDb = await Fixture.PlaylistRepository.GetByIdAsync(new(updatedPlaylist.Id)) ?? throw new Exception("Playlist not found");

        const string NewDescription = "I changed the description";
        fromDb.UpdateDescription(NewDescription);
        var updatedPlaylist2 = await Fixture.PlaylistRepository.UpdateAsync(fromDb);
        var fromDb2 = await Fixture.PlaylistRepository.GetByIdAsync(new(updatedPlaylist.Id, AllPlaylistFields)) ?? throw new Exception("Playlist not found");

        // Asserts
        fromDb2.Description.Should().Be(NewDescription);    
    }
}