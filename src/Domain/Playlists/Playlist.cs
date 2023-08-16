using Domain.Videos;

namespace Domain.Playlists;

public class Playlist : ImportedEntity, IAggregateRoot
{
    public Playlist(string name, string? description = null)
        : base(name, description)
    {
            
    }

    public Playlist(EntityOrigin origin)
        : base(origin)
    { } 

    public PlaylistId Id { get; private set; } = default!;

    public IReadOnlyCollection<PlaylistVideo> Videos => _videos.ToList();

    public int LinkToVideos(IEnumerable<VideoId> videoIds)
    {
        Guard.Against.Null(videoIds, nameof(videoIds));
        
        var newIds = videoIds
            .Except(_videos.Select(plvid => plvid.VideoId))
            .ToArray();

        foreach (var videoId in newIds)
        {
            _videos.Add(new PlaylistVideo(this.Id, videoId));
        }

        return newIds.Length;
    }  

    #region Private

    private Playlist() : base() 
    { }

    readonly List<PlaylistVideo> _videos = new();    

    #endregion
}