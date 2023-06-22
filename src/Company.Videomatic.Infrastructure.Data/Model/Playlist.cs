using System.Collections.Immutable;

namespace Company.Videomatic.Infrastructure.Data.Model;

public class Playlist : EntityBase
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; } = default!;

    public Playlist(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public IReadOnlyCollection<Video> Videos
    {
        get => _videos.ToImmutableList();
        private set => _videos = value.ToList();
    }
    public IReadOnlyCollection<PlaylistVideo> PlaylistVideos
    {
        get => _playlistVideos.ToImmutableList();
        private set => _playlistVideos = value.ToList();
    }

    #region Private

    List<Video> _videos = new List<Video>();
    List<PlaylistVideo> _playlistVideos = new List<PlaylistVideo>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Playlist()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    #endregion
}
