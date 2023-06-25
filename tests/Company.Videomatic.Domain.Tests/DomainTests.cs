namespace Company.Videomatic.Domain.Tests;

public class DomainTests
{

    [Fact]
    public void T01_CreatePlaylistWithACompleteVideo()
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
    }
}