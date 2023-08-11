using Company.Videomatic.Domain.Aggregates.Video;

namespace Company.Videomatic.Domain.Aggregates.Playlist;

public record PlaylistOrigin(
    string Id,
    string ETag,
    string ChannelId,

    string Name,
    string? Description,

    DateTime? PublishedAt,

    string? ThumbnailUrl,
    string? PictureUrl,

    string? EmbedHtml,
    string? DefaultLanguage);

public class Playlist : IEntity, IAggregateRoot
{
    public static Playlist Create(PlaylistOrigin origin)
    {
        if (origin is null)
        {
            throw new ArgumentNullException(nameof(origin));
        }

        return new Playlist
        {
            Name = origin.Name,
            Description = origin.Description,
            Origin = origin,            
        };
    }

    public static Playlist Create(string name, string? description = null)
    {
        return new Playlist
        {
            Name = name,
            Description = description,            
        };
    }

    public PlaylistId Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsStarred { get; private set; } = false;    

    public PlaylistOrigin? Origin { get; private set; } = default!;

    public IReadOnlyCollection<PlaylistVideo> Videos => _videos.ToList();

    public int LinkToVideos(IEnumerable<VideoId> videoIds)
    {
        Guard.Against.Null(videoIds, nameof(videoIds));

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

    //[JsonConstructor]
    //private Playlist(PlaylistId id, string name, bool isStarred, string? description, List<PlaylistVideo> videos) 
    //{
    //    Id = id;
    //    Name = name;
    //    IsStarred = isStarred;
    //    Description = description;
    //    _videos = videos;
    //}

    readonly List<PlaylistVideo> _videos = new();

    int IEntity.Id => this.Id;

    #endregion
}