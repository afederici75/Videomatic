namespace Company.Videomatic.Domain.Aggregates.Video;

public class Video : ImportedEntity<VideoId>, IAggregateRoot
{
    public Video(string name, string? description = null)
        : base(name, description)
    {

    }

    public Video(EntityOrigin origin)
        : base(origin)
    { }
    

    public IReadOnlyCollection<VideoTag> Tags => _videoTags.ToList();
    
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

    #region Private

    private Video()
    { }

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
