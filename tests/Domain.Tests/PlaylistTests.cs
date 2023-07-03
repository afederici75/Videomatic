namespace Domain.Tests;

/// <summary>
/// Tests that ensure domain objects are well designed.
/// </summary>
public class PlaylistTests
{
    [Fact]
    public void CreatePlaylist()
    {
        var playlist = Playlist.Create(nameof(CreatePlaylist), "A description");

        playlist.Should().NotBeNull();
        playlist.Id.Should().BeNull(); // Not assigned yet.
        playlist.Name.Should().Be(nameof(CreatePlaylist));
        playlist.Description.Should().Be("A description");
    }
}
