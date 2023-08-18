using Ardalis.Specification;
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
        
        try
        {
            var currIds = _videos.Select(_ => _.VideoId);

            var newIds = videoIds
                .Except(currIds)
                .ToArray();

            foreach (var videoId in newIds)
            {
                _videos.Add(new PlaylistVideo(this.Id, videoId));
            }

            return newIds.Length;
        }
        catch (Exception ex) 
        {

            throw;
        }
    }  

    #region Private

    private Playlist() : base() 
    { }

    readonly List<PlaylistVideo> _videos = new();    

    #endregion
}