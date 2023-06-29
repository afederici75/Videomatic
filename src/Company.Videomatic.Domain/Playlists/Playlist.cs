namespace Company.Videomatic.Domain.Playlists;

public class Playlist : EntityBase
{
    public static Playlist Create(string name, string? description)
    {
        return new Playlist {
            Name = name,
            Description = description
        };
    }

    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public IReadOnlyCollection<Videos.Video> Videos
    {
        get => _videos.ToList();
        //private set => _videos = value.ToList();
    }
    public IReadOnlyCollection<PlaylistVideo> PlaylistVideos
    {
        get => _playlistVideos.ToList();
    }

    public Video AddVideo(string location, string title, VideoDetails details, string? description)
    {
        var video = Video.Create(location, title, details, description);
        _videos.Add(video);
        return video;
    }

    // I removed this as it creates more problems than not.
    //public Playlist AddPlaylistVideo(long videoId)
    //{
    //    var newItem = PlaylistVideo.Create(Id, videoId);
    //    _playlistVideos.Add(newItem);
    //    return this;
    //}

    #region Private

    private List<Videos.Video> _videos = new List<Videos.Video>();
    private List<PlaylistVideo> _playlistVideos = new List<PlaylistVideo>();

    private Playlist()
    {
        // For entity framework
    }

    #endregion
}
