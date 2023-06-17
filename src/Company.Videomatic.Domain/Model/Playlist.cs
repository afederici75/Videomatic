namespace Company.Videomatic.Domain.Model;

public class Playlist : EntityBase, IAggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }

    [JsonIgnore]
    public IEnumerable<Video> Videos => _videos.AsReadOnly();

    public Playlist(string name, string? description = default)
        : base()
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
    }

    #region Methods

    public Playlist AddVideo(Video video)
    {
        _videos.Add(video ?? throw new ArgumentNullException(nameof(video)));

        return this;
    }

    public Playlist ClearVideos()
    {
        _videos.Clear();

        return this;
    }

    public Playlist UpdateName(string newName)
    {
        Name = Guard.Against.NullOrWhiteSpace(newName, nameof(newName));

        return this;
    }

    public Playlist UpdateDescription(string? newDescription)
    {
        Description = newDescription;

        return this;
    }

    #endregion

    #region Private

    [JsonProperty(PropertyName = nameof(Videos))]
    readonly internal List<Video> _videos = new List<Video>();

    [JsonConstructor]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Playlist()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    #endregion
}