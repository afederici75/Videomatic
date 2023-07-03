using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Transcript;

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

        playlist.Should().NotBeNull();          
    }

    [Fact]
    public void CreateDuplicateTagsIsImpossible()
    {
        var video = Video.Create(location: "youtube.com/v?VCompleteA", title: nameof(CreateVideoWithAllDetails),
            details: new("YOUTUBE", DateTime.UtcNow, "#channelId", "#playlist", 1, "#videoOwnerChannelTitle", "#videoOwnerChannelId"),
            description: "A complete description");

        video.AddTag("TAG1");
        video.AddTag("TAG2");

        video.Tags.Count.Should().Be(2);

        video.AddTag("TAG1");
        video.Tags.Count.Should().Be(2);
    }

    [Fact]
    public void CreateVideoWithAllDetails()
    {
        var video = Video.Create(location: "youtube.com/v?VCompleteA", title: nameof(CreateVideoWithAllDetails),
            details: new("YOUTUBE", DateTime.UtcNow, "#channelId", "#playlist", 1, "#videoOwnerChannelTitle", "#videoOwnerChannelId"),
            description: "A complete description");

        video.AddThumbnail(location: "youtubethumbs.com/T1_1", resolution: ThumbnailResolution.Default, height: 100, width: 100);
        video.AddThumbnail(location: "youtubethumbs.com/T1_2", resolution: ThumbnailResolution.Medium, height: 200, width: 200);

        video.AddTag("Tag1");
        video.AddTag("Tag2");
    }


    [Fact]
    public void CreateArtifact()
    {
        var artifact = Artifact.Create(1, title: "A complete summary", type: "AI", text: "Bla bla");        
        artifact.Should().NotBeNull();
    }

    [Fact]
    public void CreateTranscript()
    {
        var transcript = Transcript.Create(1, "EN", new[] 
        { 
            "First line",
            "Second line"
        });

        var trans1 = Transcript.Create(1, "EN");
        trans1.AddLine(text: "This is a line with nothing");
        trans1.AddLine(text: "This is a line with something", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2));
    }

    [Fact]
    public void LinkVideoToPlaylists()
    {
        var playlist = Playlist.Create(name: nameof(LinkVideoToPlaylists), description: $"A playlist with 1 complete videos {DateTime.Now}");
        var video = Video.Create("http://somewhere", "Title",
            details: new("YOUTUBE", DateTime.UtcNow, "#channelId", "#playlist", 1, "#videoOwnerChannelTitle", "#videoOwnerChannelId"), 
            "Video description");
        
        video.AddThumbnail(location: "youtubethumbs.com/T1_1", resolution: ThumbnailResolution.Default, height: 100, width: 100);
        
        video.LinkToPlaylists(playlist.Id);

        // Checks
        video.Playlists.Count.Should().Be(1);
        video.Thumbnails.Count.Should().Be(1);
    }
}