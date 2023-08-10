namespace Domain.Tests;

/// <summary>
/// Tests that ensure domain objects are well designed.
/// </summary>
public class PlaylistTests
{
    const string DummyLocation = "youtube.com/v?VCompleteA";

    [Fact]
    public void CreatePlaylist()
    {
        var playlist = Playlist.Create(nameof(CreatePlaylist), "A description");

        playlist.Should().NotBeNull();
        playlist.Id.Should().BeNull(); // Not assigned yet.
        playlist.Name.Should().Be(nameof(CreatePlaylist));
        playlist.Description.Should().Be("A description");
    }

    [Fact]
    public void LinkVideoToPlaylists()
    {
        var playlist = Playlist.Create(nameof(CreatePlaylist), "A description");
        var count = playlist.LinkToVideos(new VideoId[] { 123, 342 });
        
        // Checks
        playlist.Videos.Count.Should().Be(2);        
    }

    [Fact]
    public void DoesNotLinkVideoToNullPlaylistId()
    {
        var playlist = Playlist.Create(name: nameof(LinkVideoToPlaylists));
        var video = Video.Create(DummyLocation, "http://1", "http://2", nameof(DoesNotLinkVideoToNullPlaylistId));
        
        #pragma warning disable CS8620 
        // If you pass a newly created Playlist (which has a null Id) it would be null.
        // Nulls should be ignored.
        var count = playlist.LinkToVideos(new [] { default(VideoId) });
        #pragma warning restore CS8620 

        // Checks
        playlist.Videos.Count.Should().Be(0);
        count.Should().Be(0);
    }
}
