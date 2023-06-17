using Company.Videomatic.Domain.Model;

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
    public async Task CreatesEmptyPlaylist()
    {
        // Prepares
        var newPlaylist = new Playlist("My playlist", $"A description for my playlist generated at {DateTime.Now}");        

        // Executes
        var updatedPlaylist = await Fixture.PlaylistRepository.AddAsync(newPlaylist);
        
        // Asserts
        newPlaylist.Id.Should().Be(0);
        updatedPlaylist.Id.Should().BeGreaterThan(0);

        var fromDbPlaylist = await Fixture.PlaylistRepository.GetByIdAsync(new (updatedPlaylist.Id));
        fromDbPlaylist.Should().NotBeNull();
        fromDbPlaylist.Should().BeEquivalentTo(updatedPlaylist);        
    }

    [Fact]
    public async Task CreatesPlaylistWithTwoVideos()
    {
        // Prepares
        var newPlaylist = new Playlist(name: "My playlist", description: $"A playlist with 2 videos generated at {DateTime.Now}");
        var vid1 = new Video(location: "youtube.com/v?V1", title: "A title", description: "A description");        
        var vid2 = new Video(location: "youtube.com/v?V2", title: "A second title", description: "A second description");

        newPlaylist.AddVideo(vid1)
                   .AddVideo(vid2);

        // Executes
        var updatedPlaylist = await Fixture.PlaylistRepository.AddAsync(newPlaylist);

        // Asserts
        updatedPlaylist.Id.Should().BeGreaterThan(0);

        var fromDbPlaylist = await Fixture.PlaylistRepository.GetByIdAsync(new (updatedPlaylist.Id));
        fromDbPlaylist.Should().NotBeNull();
        fromDbPlaylist!.Videos.Should().HaveCount(0); // We did not specify any includes        
    }

    [Fact]
    public async Task CreatePlaylistWithACompleteVideo()
    {
        // Prepares
        var newPlaylist = new Playlist(name: "My playlist", description: $"A playlist with 2 complete videos generated at {DateTime.Now}");
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
        var updatedPlaylist = await Fixture.PlaylistRepository.AddAsync(newPlaylist);

        // Asserts
        newPlaylist.Id.Should().Be(0);
        updatedPlaylist.Id.Should().BeGreaterThan(0);

        var fromDbPlaylist2 = await Fixture.PlaylistRepository.GetByIdAsync(new(updatedPlaylist.Id, 
            new[] { nameof(Playlist.Videos), "Videos.Thumbnails", "Videos.Tags", "Videos.Artifacts", "Videos.Transcripts", "Videos.Transcripts.Lines" }));

        fromDbPlaylist2.Should().NotBeNull();
        fromDbPlaylist2!.Videos.Should().HaveCount(newPlaylist.Videos.Count);
        foreach (var vid in fromDbPlaylist2!.Videos)
        {
            vid.Thumbnails.Should().NotBeEmpty();
            vid.Tags.Should().NotBeEmpty();
            vid.Artifacts.Should().NotBeEmpty();            
            vid.Transcripts.Should().NotBeEmpty();
            vid.Transcripts.First().Lines.Should().BeEmpty();    
        }
        fromDbPlaylist2.Should().BeEquivalentTo(updatedPlaylist);
    }
}