namespace Company.Videomatic.Domain.Model;

public class Tag : EntityBase
{
    public string Name { get; private set; }

    public IReadOnlyCollection<Video> Videos
    {
        get => _videos.ToImmutableList();
        private set => _videos = value.ToList();
    }

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

    List<Video> _videos = new List<Video>();

    #endregion
}
