namespace Domain.Playlists.Tests;

/// <summary>
/// Tests that ensure domain objects are well designed.
/// </summary>
public class PlaylistTests
{
    const string DummyLocation = "youtube.com/v?VCompleteA";

    [Fact]
    public void CreatePlaylist()
    {
        var playlist = new Playlist(nameof(CreatePlaylist), "A description");

        playlist.Should().NotBeNull();
        playlist.Id.Should().BeNull(); // Not assigned yet.
        playlist.Name.Should().Be(nameof(CreatePlaylist));
        playlist.Description.Should().Be("A description");
    }

    [Fact]
    public void LinkVideoToPlaylists()
    {
        var playlist = new Playlist(nameof(LinkVideoToPlaylists), "A description");
        var count = playlist.LinkToVideos(new VideoId[] { 123, 342 });
        
        // Checks
        playlist.Videos.Count.Should().Be(2);        
    }

    [Fact]
    public void DoesNotLinkVideoToNullPlaylistId()
    {
        var playlist = new Playlist(nameof(DoesNotLinkVideoToNullPlaylistId));

#pragma warning disable CS8620
        // If you pass a newly created Playlist (which has a null Id) it would be null.
        // Nulls should be ignored.
        Assert.Throws<ArgumentNullException>(() => playlist.LinkToVideos(new[] { default(VideoId) })); 
    
#pragma warning restore CS8620 

        // Checks
        playlist.Videos.Count.Should().Be(0);        
    }
}
