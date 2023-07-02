using Company.Videomatic.Domain.Aggregates.Playlist;

namespace Company.Videomatic.Domain.Aggregates.Video;

public class Video : IAggregateRoot
{
    public static Video Create(string location, string title, VideoDetails details, string? description)
    {
        return new Video
        {
            Location = location,
            Name = title,
            Description = description,
            Details = details ?? VideoDetails.CreateEmpty()
        };
    }

    public VideoId Id { get; private set; } = default!;
    public string Location { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public VideoDetails Details { get; private set; } = default!;

    public IReadOnlyCollection<VideoTag> VideoTags => _videoTags.ToList();
    public IReadOnlyCollection<Thumbnail> Thumbnails => _thumbnails.ToList();
    public IReadOnlyCollection<PlaylistVideo> Playlists => _playlists.ToList();

    public bool AddTag(string name)
    {
        return _videoTags.Add(name);
    }

    public void AddThumbnail(string location, ThumbnailResolution resolution, int height, int width)
    {
        var res = _thumbnails.FirstOrDefault(t => t.Resolution == resolution);
        if (res is not null)
        {
            _thumbnails.Remove(res);
        }

        _thumbnails.Add(new Thumbnail(location, resolution, height, width));
    }

    public int LinkToPlaylists(params PlaylistId[] playlistIds)
    {
        var goodIds = playlistIds
            .Except(_playlists.Select(p => p.PlaylistId))
            .ToArray();

        foreach (var playlistId in goodIds)
        {
            _playlists.Add(PlaylistVideo.Create(playlistId, Id));            
        }
        return goodIds.Length;
    }


    #region Private

    private Video()
    { }

    HashSet<VideoTag> _videoTags = new();
    HashSet<Thumbnail> _thumbnails = new();

    List<PlaylistVideo> _playlists = new();    

    #endregion
}
