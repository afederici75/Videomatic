using Ardalis.GuardClauses;
using Newtonsoft.Json;

namespace Company.Videomatic.Domain.Aggregates.Video;

public class Video : IEntity, IAggregateRoot
{
    public static Video Create(string location, string name, Thumbnail picture, Thumbnail thumbnail, VideoDetails? details = null, string? description = null)
    {
        return new Video
        {
            Location = location,
            Name = name,
            Description = description,
            Details = details ?? VideoDetails.CreateEmpty(),
            Picture = picture,
            Thumbnail = thumbnail,            
        };
    }


    public VideoId Id { get; private set; } = default!;
    public string Location { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public bool IsStarred { get; private set; } = false;
    public string? Description { get; private set; }
    public VideoDetails Details { get; private set; } = default!;

    public IReadOnlyCollection<VideoTag> Tags => _videoTags.ToList();
    
    public Thumbnail Thumbnail { get; private set; } = default!;
    public Thumbnail Picture { get; private set; } = default!;

    public void ClearTags()
    {
        _videoTags.Clear();
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

    public void ToggleStarred()
    { 
        IsStarred = !IsStarred;
    }

    
    #region Private
    
    private Video()
    {       
    }

    //[JsonConstructor]
    //private Video(VideoId id, string location, string name, bool isStarred, string? description, VideoDetails details, HashSet<VideoTag> tags, HashSet<Thumbnail> thumbnails)
    //{
    //    Id = id;
    //    Location = location;
    //    Name = name;
    //    IsStarred = isStarred;
    //    Description = description;
    //    Details = details;
    //    _videoTags = tags;
    //    _thumbnails = thumbnails;        
    //}

    int IEntity.Id => this.Id;

    readonly HashSet<VideoTag> _videoTags = new();
    //HashSet<Thumbnail> _thumbnails = new(); // TODO: not readonly == code smell?

    #endregion
}
