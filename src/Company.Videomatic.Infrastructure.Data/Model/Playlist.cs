using System.Collections.Immutable;

namespace Company.Videomatic.Infrastructure.Data.Model;

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
    public string? Description { get; private set; } = default!;

    public IReadOnlyCollection<Video> Videos
    {
        get => _videos.ToList();
        //private set => _videos = value.ToList();
    }
    public IReadOnlyCollection<PlaylistVideo> PlaylistVideos
    {
        get => _playlistVideos.ToList();
        //private set => _playlistVideos = value.ToList();
    }

    public Playlist AddVideo(Video video)
    {
        _videos.Add(video);
        return this;
    }

    public Playlist AddPlaylistVideo(long videoId)
    {
        var newItem = PlaylistVideo.Create(Id, videoId);
        _playlistVideos.Add(newItem);
        return this;
    }

    #region Private

    private List<Video> _videos = new List<Video>();
    private List<PlaylistVideo> _playlistVideos = new List<PlaylistVideo>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Playlist()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    #endregion
}
