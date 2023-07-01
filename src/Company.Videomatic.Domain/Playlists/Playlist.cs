namespace Company.Videomatic.Domain.Playlists;

public record PlaylistId(long Value = 0)
{
    public static implicit operator long(PlaylistId x) => x.Value;
    public static implicit operator PlaylistId(long x) => new PlaylistId(x);
}

public class Playlist : IAggregateRoot
{
    public static Playlist Create(string name, string? description)
    {
        return new Playlist {
            Name = name,
            Description = description
        };
    }

    public PlaylistId Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public void AddVideo(VideoId videoId)
    {        
        _playlistVideos.Add(PlaylistVideo.Create(Id, videoId));
    }
    
    #region Private

    private List<PlaylistVideo> _playlistVideos = new List<PlaylistVideo>();

    private Playlist()
    {
        // For entity framework
    }

    #endregion
}
