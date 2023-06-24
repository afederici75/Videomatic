using Company.Videomatic.Application.Features.DataAccess;
using Company.Videomatic.Application.Features.Model;
using Company.Videomatic.Application.Features.Playlists.Commands;
using Company.Videomatic.Application.Features.Playlists.Queries;
using Company.Videomatic.Application.Features.Videos.Commands;
using Company.Videomatic.Application.Features.Videos.Queries;
using Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;
using MediatR;

namespace Company.Videomatic.Infrastructure.Data.Tests.SqlServer;

[Collection("DbContextTests")]
public class SqlServerPlaylistsTests : IClassFixture<SqlServerDbContextFixture>
{
    public SqlServerPlaylistsTests(
        SqlServerDbContextFixture fixture,
        ISender sender)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));

        Fixture.SkipDeletingDatabase = true;
    }

    public SqlServerDbContextFixture Fixture { get; }
    public ISender Sender { get; }

    [Fact]
    public async Task T01_CreatePlaylist()
    {
        // Creates the playlist
        var createCmd = new CreatePlaylistCommand(Name: "My playlist 1", Description: $"A description for my playlist {DateTime.Now}");                
        CreatedResponse createResponse = await Sender.Send(createCmd);

        // Checks
        createResponse.Id.Should().BeGreaterThan(0);

        var qry = new GetPlaylistsQuery($"Id=={createResponse.Id}");
        PageResult<PlaylistDTO> getByIdResponse = await Sender.Send(qry);
        
        var playlist = getByIdResponse.Items.Single();
        playlist.Id.Should().Be(createResponse.Id);   
        playlist.Name.Should().Be(createCmd.Name);
        playlist.Description.Should().Be(createCmd.Description);
    }

    [Fact]
    public async Task T02_CreatesPlaylistWithTwoVideos()
    {
        // Executes
        var createPlaylistCmd = new CreatePlaylistCommand(Name: "My playlist 2", Description: $"A description for my playlist {DateTime.Now}");
        CreatedResponse createPlaylistResponse = await Sender.Send(createPlaylistCmd);

        var createVid1Cmd = new CreateVideoCommand(Location: "youtube.com/v?V1", Title: "A title", Description: "A description");
        CreatedResponse createVid1Response = await Sender.Send(createVid1Cmd);

        var createVid2Cmd = new CreateVideoCommand(Location: "youtube.com/v?V2", Title: "A second title", Description: "A second description");
        CreatedResponse createVid2Response = await Sender.Send(createVid2Cmd);

        var addVidsCmd = new LinkVideosToPlaylistCommand(createPlaylistResponse.Id, VideoIds: new[] {  createVid1Response.Id, createVid2Response.Id });
        LinkVideosToPlaylistResponse addVidsResponse = await Sender.Send(addVidsCmd); // Should add 2 videos
        LinkVideosToPlaylistResponse emptyAddVidsResponse = await Sender.Send(addVidsCmd); // Should not add anything as they are both dups
    
        // Checks
        createPlaylistResponse.Id.Should().BeGreaterThan(0);
        createVid1Response.Id.Should().BeGreaterThan(0);
        createVid2Response.Id.Should().BeGreaterThan(0);

        var filter = $"Id in ({createVid1Response.Id}, {createVid2Response.Id})";
        var videosResponse = await Sender.Send(new GetVideosQuery(Filter: filter));
        videosResponse. Items.Should().HaveCount(2);
        
        emptyAddVidsResponse.PlaylistId.Should().Be(createPlaylistResponse.Id);
        emptyAddVidsResponse.VideoIds.Should().BeEmpty();   
    }
    
    
    [Fact]
    public async Task T03_CreatePlaylistThenUpdateThenDeleteIt()
    {
        // Creates the playlist
        var createPlaylistCmd = new CreatePlaylistCommand(Name: "My playlist 2", Description: $"A description for my playlist {DateTime.Now}");
        CreatedResponse createPlaylistResponse = await Sender.Send(createPlaylistCmd);        
    
        // Updates the playlist
        var updatePlaylistCmd = new UpdatePlaylistCommand(Id: createPlaylistResponse.Id, Name: "Replaced", Description: $"Changed");
        UpdatedResponse updatePlaylistResponse = await Sender.Send(updatePlaylistCmd);
        
        // Verifies
        updatePlaylistResponse.Updated.Should().BeTrue();
        
        var qry = new GetPlaylistsQuery($"Id==({createPlaylistResponse.Id})");
        PageResult<PlaylistDTO> getResponse = await Sender.Send(qry);
        
        var record = getResponse.Items.Single();
        record.Id.Should().Be(createPlaylistResponse.Id);   
        record.Name.Should().Be(updatePlaylistCmd.Name);
        record.Description.Should().Be(updatePlaylistCmd.Description);
        
        // Deletes
        var deleteCmd = new DeletePlaylistCommand(createPlaylistResponse.Id);
        DeletedResponse deleteResponse = await Sender.Send(deleteCmd);
        deleteResponse.Deleted.Should().BeTrue();
        deleteResponse.Id.Should().Be(createPlaylistResponse.Id);
        
        getResponse = await Sender.Send(qry);
        getResponse.Items.Should().BeEmpty();
    }

    //readonly string[] AllPlaylistFields = new[] { nameof(Playlist.Videos), "Videos.Thumbnails", "Videos.Tags", "Videos.Artifacts", "Videos.Transcripts", "Videos.Transcripts.Lines" };

    [Fact]
    public async Task T04_CreatePlaylistWithACompleteVideo()
    {
       // Prepares
       var newPlaylist = Playlist.Create(name: "My playlist 3", description: $"A playlist with 2 complete videos {DateTime.Now}");
       var vid1 = Video.Create(location: "youtube.com/v?VCompleteA", title: "A complete title", description: "A complete description");

       var thumb1 = vid1.AddThumbnail(location: "youtubethumbs.com/T1_1", resolution: ThumbnailResolution.Default, height: 100, width: 100);
       var thumb2 = vid1.AddThumbnail(location: "youtubethumbs.com/T1_2", resolution: ThumbnailResolution.Medium, height: 200, width: 200);

       var tag1 = vid1.AddTag("Tag1");
       var tag2 = vid1.AddTag("Tag2");

       var arti1 = vid1.AddArtifact(title: "A complete summary", type: "AI", text: "Bla bla");
       var arti2 = vid1.AddArtifact(title: "A complete analysis", type: "AI", text: "More bla bla");
       
       var trans1 = vid1.AddTranscript("EN");
       var linet1_1 = trans1.AddLine(text: "This is", startsAt: TimeSpan.FromSeconds(0), duration: TimeSpan.FromSeconds(1));
       var linet1_2 = trans1.AddLine(text: "a long transcript", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2));

       var trans2 = vid1.AddTranscript("IT");
       var linet2_1 = trans2.AddLine(text: "Questa e'", startsAt: TimeSpan.FromSeconds(1), duration: TimeSpan.FromSeconds(1));
       var linet2_2 = trans2.AddLine(text: "una lunga transcrizione", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2));
       
       
       newPlaylist.AddVideo(vid1);

        // Executes
        //var updatedPlaylist = await Fixture.CommandsHandler.CreateAsync(newPlaylist);
        //
        //// Asserts
        //newPlaylist.Id.Should().Be(0);
        //updatedPlaylist.Id.Should().BeGreaterThan(0);
        //
        //var fromDbPlaylist = await Fixture.CommandsHandler.GetByIdAsync(new(updatedPlaylist.Id, AllPlaylistFields));
        //
        //fromDbPlaylist.Should().NotBeNull();
        //fromDbPlaylist!.Videos.Should().HaveCount(newPlaylist.Videos.Count);
        //foreach (var vid in fromDbPlaylist!.Videos)
        //{
        //    vid.Thumbnails.Should().NotBeEmpty();
        //    vid.Tags.Should().NotBeEmpty();
        //    vid.Artifacts.Should().NotBeEmpty();            
        //    vid.Transcripts.Should().NotBeEmpty();
        //    vid.Transcripts.First().Lines.Should().NotBeEmpty();    
        //}
        //
        // If I use Should().BeEquivalentTo I get the following error. I assume it is because of Video.Playlists...
        //    Expected fromDbPlaylist2.Videos[0].Tags[0].Videos[0] to be [youtube.com/v?VCompleteA, Thumbnails: 2, Transcripts: 2] A complete title, but it contains a cyclic reference.
        //    Expected fromDbPlaylist2.Videos[0].Tags[1].Videos[0] to be[youtube.com / v ? VCompleteA, Thumbnails: 2, Transcripts: 2] A complete title, but it contains a cyclic reference.
        //    Expected fromDbPlaylist2.Videos[0].Playlists[0] to be Company.Videomatic.Domain.Model.Playlist
        //    { JSON }, but it contains a cyclic reference.
        //fromDbPlaylist2.Should().BeEquivalentTo(updatedPlaylist);
    }

    [Fact]
    public async Task T05_CreateNonEmptyPlaylistAndUpdatesIt()
    {
        // Prepares
        //var newPlaylist = new Playlist(name: "My playlist 4", description: $"A playlist with 2 complete videos {DateTime.Now}");
        //var vid1 = new Video(location: "youtube.com/v?VCompleteA", title: "A complete title", description: "A complete description");
        //
        //vid1.AddThumbnail(new Thumbnail(location: "youtubethumbs.com/T1_1", resolution: ThumbnailResolution.Default, height: 100, width: 100))
        //    .AddThumbnail(new Thumbnail(location: "youtubethumbs.com/T1_2", resolution: ThumbnailResolution.Medium, height: 200, width: 200));
        //
        //vid1.AddTag(new Tag("Tag1"))
        //    .AddTag(new Tag("Tag2"));
        //
        //vid1.AddArtifact(new Artifact(title: "A complete summary", type: "AI", text: "Bla bla"))
        //    .AddArtifact(new Artifact(title: "A complete analysis", type: "AI", text: "More bla bla"));
        //
        //var trans1 = new Transcript("EN");
        //trans1.AddLine(new TranscriptLine(text: "This is", startsAt: TimeSpan.FromSeconds(0), duration: TimeSpan.FromSeconds(1)));
        //trans1.AddLine(new TranscriptLine(text: "a long transcript", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2)));
        //
        //var trans2 = new Transcript("IT");
        //trans1.AddLine(new TranscriptLine(text: "Questa e'", startsAt: TimeSpan.FromSeconds(1), duration: TimeSpan.FromSeconds(1)));
        //trans1.AddLine(new TranscriptLine(text: "una lunga transcrizione", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2)));
        //
        //vid1.AddTranscript(trans1)
        //    .AddTranscript(trans2);
        //
        //newPlaylist.AddVideo(vid1);

        //// Executes
        //var updatedPlaylist = await Fixture.CommandsHandler.CreateAsync(newPlaylist);
        //var fromDb = await Fixture.CommandsHandler.GetByIdAsync(new(updatedPlaylist.Id)) ?? throw new Exception("Playlist not found");
        //
        //const string NewDescription = "I changed the description";
        //fromDb.UpdateDescription(NewDescription);
        //var updatedPlaylist2 = await Fixture.CommandsHandler.UpdateAsync(fromDb);
        //var fromDb2 = await Fixture.CommandsHandler.GetByIdAsync(new(updatedPlaylist.Id, AllPlaylistFields)) ?? throw new Exception("Playlist not found");
        //
        //// Asserts
        //fromDb2.Description.Should().Be(NewDescription);    
    }
}