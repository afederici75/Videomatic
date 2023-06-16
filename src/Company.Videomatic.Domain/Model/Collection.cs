namespace Company.Videomatic.Domain.Model;

public class Collection : EntityBase, IAggregateRoot
{ 
    public string Name { get; set; }
    public string Url { get; set; }
    public string? Description { get; set; }

    [JsonIgnore]
    public IEnumerable<Video> Videos => _videos.AsReadOnly();

    [JsonIgnore]
    public IEnumerable<Tag> Tags => _tags.AsReadOnly();

    public Collection(string name, string url, string? description = default, IEnumerable<Video>? videos = default)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Url = url ?? throw new ArgumentNullException(nameof(url));
        Description = description;
        _videos = videos?.ToList() ?? _videos;
    }

    public Collection AddVideos(params Video[] videos)
    {
        _videos.AddRange(videos);

        return this;
    }

    public Collection ClearVideos()
    {
        _videos.Clear();

        return this;
    }

    public Collection AddTags(params Tag[] tags)
    {
        _tags.AddRange(tags);

        return this;
    }

    public Collection ClearTags()
    {
        _tags.Clear();

        return this;
    }

    #region Private

    [JsonProperty(PropertyName = nameof(Videos))]
    private List<Video> _videos = new List<Video>();

    [JsonProperty(PropertyName = nameof(Tags))]
    private List<Tag> _tags = new List<Tag>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    private Collection()
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    #endregion
}
