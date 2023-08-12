using Company.Videomatic.Domain.Aggregates.Video;

namespace Company.Videomatic.Domain.Aggregates.Playlist;

public class Playlist : ImportedEntity<PlaylistId>, IAggregateRoot
{
    public Playlist(string name, string? description = null)
        : base(name, description)
    {
            
    }

    public Playlist(EntityOrigin origin)
        : base(origin)
    { } 

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

    #region Private

    private Playlist() : base() { }

    readonly List<PlaylistVideo> _videos = new();    

    #endregion
}