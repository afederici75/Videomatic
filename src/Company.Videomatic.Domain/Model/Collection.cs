namespace Company.Videomatic.Domain.Model;

public class Collection : EntityBase
{ 
    public string Name { get; init; }
    public string Url { get; init; }
    public string Description { get; init; }

    [JsonIgnore]
    public IEnumerable<Video> Videos => _videos.AsReadOnly();

    [JsonIgnore]
    public IEnumerable<Tag> Tags => _tags.AsReadOnly();

    public Collection(string name, string url, IEnumerable<Video>? videos = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Url = url ?? throw new ArgumentNullException(nameof(url));
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
