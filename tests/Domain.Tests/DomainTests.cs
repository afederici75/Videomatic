namespace Domain.Tests;

/// <summary>
/// Tests that ensure domain objects are well designed.
/// </summary>
public class DomainTests
{
    [Fact]
    public void CreatePlaylist()
    {
        var playlist = Playlist.Create(nameof(CreatePlaylist), "A description");

        playlist.Videos.Should().HaveCount(0);
        playlist.PlaylistVideos.Should().HaveCount(0);        
    }

    [Fact]
    public void CreateDuplicateTagsIsImpossible()
    {
        var video = Video.Create(location: "youtube.com/v?VCompleteA", title: nameof(CreateVideoWithAllDetails),
            details: new("YOUTUBE", DateTime.UtcNow, "#channelId", "#playlist", 1, "#videoOwnerChannelTitle", "#videoOwnerChannelId"),
            description: "A complete description");

        video.AddTag("TAG1");
        video.AddTag("TAG2");

        video.VideoTags.Count.Should().Be(2);

        video.AddTag("TAG1");
        video.VideoTags.Count.Should().Be(2);
    }

    [Fact]
    public void CreateVideoWithAllDetails()
    {
        var video = Video.Create(location: "youtube.com/v?VCompleteA", title: nameof(CreateVideoWithAllDetails), 
            details: new ("YOUTUBE", DateTime.UtcNow, "#channelId", "#playlist", 1, "#videoOwnerChannelTitle", "#videoOwnerChannelId"),
            description: "A complete description");

        video.AddThumbnail(location: "youtubethumbs.com/T1_1", resolution: ThumbnailResolution.Default, height: 100, width: 100);
        video.AddThumbnail(location: "youtubethumbs.com/T1_2", resolution: ThumbnailResolution.Medium, height: 200, width: 200);

        video.AddTag("Tag1");
        video.AddTag("Tag2");

        var arti1 = video.AddArtifact(title: "A complete summary", type: "AI", text: "Bla bla");
        var arti2 = video.AddArtifact(title: "A complete analysis", type: "AI", text: "More bla bla");

        var trans1 = video.AddTranscript("EN");
        var linet1_1 = trans1.AddLine(text: "This is", startsAt: TimeSpan.FromSeconds(0), duration: TimeSpan.FromSeconds(1));
        var linet1_2 = trans1.AddLine(text: "a long transcript", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2));

        var trans2 = video.AddTranscript("IT");
        var linet2_1 = trans2.AddLine(text: "Questa e'", startsAt: TimeSpan.FromSeconds(1), duration: TimeSpan.FromSeconds(1));
        var linet2_2 = trans2.AddLine(text: "una lunga transcrizione", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2));        
    }

    [Fact]
    public void CreatePlaylistWithVideo()
    {
        var playlist = Playlist.Create(name: nameof(CreatePlaylistWithVideo), description: $"A playlist with 1 complete videos {DateTime.Now}");
        var video = Video.Create("http://somewhere", "Title",
            details: new("YOUTUBE", DateTime.UtcNow, "#channelId", "#playlist", 1, "#videoOwnerChannelTitle", "#videoOwnerChannelId"), 
            "Video description");

        video.AddThumbnail(location: "youtubethumbs.com/T1_1", resolution: ThumbnailResolution.Default, height: 100, width: 100);

        // Checks
        playlist.Videos.Count.Should().Be(1);
        playlist.PlaylistVideos.Count.Should().Be(0);
        video.Thumbnails.Count.Should().Be(1);

    }
}