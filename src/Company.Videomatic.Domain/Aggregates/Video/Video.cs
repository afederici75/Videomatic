using Ardalis.GuardClauses;
using Newtonsoft.Json;

namespace Company.Videomatic.Domain.Aggregates.Video;

public class Video : IAggregateRoot<VideoId>
{
    public static Video Create(string location, string name, VideoDetails? details = null, string? description = null)
    {
        return new Video
        {
            Location = location,
            Name = name,
            Description = description,
            Details = details ?? VideoDetails.CreateEmpty(),
            
            _thumbnails = new ()
            {
                new(ThumbnailResolution.Default, string.Empty, -1, -1),
                new(ThumbnailResolution.Standard, string.Empty, -1, -1),
                new(ThumbnailResolution.Medium, string.Empty, -1, -1),
                new(ThumbnailResolution.High, string.Empty, -1, -1),
                new(ThumbnailResolution.MaxRes, string.Empty, -1, -1),
            }
        };
    }



    public VideoId Id { get; private set; } = default!;
    public string Location { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public VideoDetails Details { get; private set; } = default!;

    public IReadOnlyCollection<VideoTag> Tags => _videoTags.ToList();
    public IReadOnlyCollection<Thumbnail> Thumbnails => _thumbnails.ToList();
    public IReadOnlyCollection<VideoPlaylist> Playlists => _playlists.ToList();

    public void ClearTags()
    {
        _videoTags.Clear();
    }    

    public Thumbnail GetThumbnail(ThumbnailResolution resolution)
    {
        return _thumbnails.First(t => t.Resolution == resolution);
    }   

    public int AddTags(params string[] names)
    {
        var cnt = 0;
        foreach (var name in names)
        {
            if (_videoTags.Add(name))
                cnt++;
        }
        return cnt;
    }

    public void SetThumbnail(ThumbnailResolution resolution, string location, int height, int width)
    {
        var res = _thumbnails.FirstOrDefault(t => t.Resolution == resolution);
        if (res is not null)
        {
            _thumbnails.Remove(res);
        }

        _thumbnails.Add(new Thumbnail(resolution, location, height, width));
    }

    public int LinkToPlaylists(PlaylistId[] playlistIds)
    {
        Guard.Against.Null(playlistIds, nameof(playlistIds));
        
        // We have _playlists fetched from the db. The 
        var goodIds = playlistIds
            .Where(pid => pid is not null)
            .Except(_playlists.Select(p => p.PlaylistId))
            .ToArray();

        foreach (var playlistId in goodIds)
        {
            _playlists.Add(VideoPlaylist.Create(playlistId, Id));            
        }
        return goodIds.Length;
    }


    #region Private
    
    private Video()
    {       
    }

    [JsonConstructor]
    private Video(VideoId id, string location, string name, string? description, VideoDetails details, HashSet<VideoTag> tags, HashSet<Thumbnail> thumbnails, List<VideoPlaylist> playlists)
    {
        Id = id;
        Location = location;
        Name = name;
        Description = description;
        Details = details;
        _videoTags = tags;
        _thumbnails = thumbnails;
        _playlists = playlists;
    }

    HashSet<VideoTag> _videoTags = new();
    HashSet<Thumbnail> _thumbnails = new();    
    List<VideoPlaylist> _playlists = new();    

    #endregion
}
