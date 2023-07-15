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
        throw new Exception("Not implemented yet");
        //var video = Video.Create(DummyLocation, nameof(LinkVideoToPlaylists));
        //var count = video.LinkToPlaylists(new PlaylistId[] { 123, 342 });
        //
        //// Checks
        //video.Playlists.Count.Should().Be(2);        
    }

    [Fact]
    public void DoesNotLinkVideoToNullPlaylistId()
    {
        throw new Exception("Not implemented yet");
        //        var playlist = Playlist.Create(name: nameof(LinkVideoToPlaylists));
        //        var video = Video.Create(DummyLocation, nameof(DoesNotLinkVideoToNullPlaylistId));

        //#pragma warning disable CS8620 
        //        // If you pass a newly created Playlist (which has a null Id) it would be null.
        //        // Nulls should be ignored.
        //        var count = video.LinkToPlaylists(new[] { default(PlaylistId) });
        //#pragma warning restore CS8620 

        //        // Checks
        //        video.Playlists.Count.Should().Be(0);
        //        count.Should().Be(0);
    }
}
