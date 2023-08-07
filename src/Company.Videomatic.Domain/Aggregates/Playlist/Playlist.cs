using Company.Videomatic.Domain.Aggregates.Video;

namespace Company.Videomatic.Domain.Aggregates.Playlist;

public class Playlist : IEntity, IAggregateRootx
{
    public static Playlist Create(string name, string? description = null)
    {
        return new Playlist
        {
            Name = name,
            Description = description
        };
    }

    public PlaylistId Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsStarred { get; private set; } = false;

    public IReadOnlyCollection<PlaylistVideo> Videos => _videos.ToList();

    public int LinkToVideos(IEnumerable<VideoId> videoIds)
    {
        Guard.Against.Null(videoIds, nameof(videoIds));

        // We have _playlists fetched from the db. The 
        var goodIds = videoIds
            .Where(vid => vid is not null) // TODO: code smell?
            .Except(_videos.Select(p => p.VideoId))
            .ToArray();

        foreach (var videoId in goodIds)
        {
            _videos.Add(PlaylistVideo.Create(this.Id, videoId));
        }
        return goodIds.Length;
    }

    public void ToggleStarred()
    {
        IsStarred = !IsStarred;
    }


    #region Private

    private Playlist() { }

    [JsonConstructor]
    private Playlist(PlaylistId id, string name, bool isStarred, string? description, List<PlaylistVideo> videos) 
    {
        Id = id;
        Name = name;
        IsStarred = isStarred;
        Description = description;
        _videos = videos;
    }

    List<PlaylistVideo> _videos = new();

    int IEntity.Id => this.Id;

    #endregion
}