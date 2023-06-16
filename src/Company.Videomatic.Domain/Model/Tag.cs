namespace Company.Videomatic.Domain.Model;

public class Tag : EntityBase
{
    public string Name { get; private set; }

    [JsonIgnore]
    public IEnumerable<Video> Videos => _videos.AsReadOnly();

    public Tag(string name)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));  
    }

    #region Methods

    public Tag UpdateName(string newName)
    {
        Name = Guard.Against.NullOrWhiteSpace(newName, nameof(newName));

        return this;
    }   

    public Tag AddVideo(Video video)
    {
        _videos.Add(Guard.Against.Null(video, nameof(video)));

        return this;
    }

    #endregion

    #region Private

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    private Tag()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }


    [JsonProperty(PropertyName = nameof(Videos))]
    private List<Video> _videos = new List<Video>();

    #endregion
}
