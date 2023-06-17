namespace Company.Videomatic.Domain.Model;

public class Video : EntityBase, IAggregateRoot
{    
    public string Location { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }

    [JsonIgnore]
    public IEnumerable<Tag> Tags => _tags.AsReadOnly();

    [JsonIgnore]
    public IEnumerable<Playlist> Collections => _collections.AsReadOnly();

    [JsonIgnore]
    public IEnumerable<Artifact> Artifacts => _artifacts.AsReadOnly();

    [JsonIgnore]
    public IEnumerable<Thumbnail> Thumbnails => _thumbnails.AsReadOnly();

    [JsonIgnore]
    public IEnumerable<Transcript> Transcripts => _transcripts.AsReadOnly();

    public Video(string location, string title, string? description = null)
    {
        Location = Guard.Against.NullOrWhiteSpace(location, nameof(location));
        Title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
        Description = description;
    }

    #region Methods

    public override string ToString()
    {
        return $"[{Location}, Thumbnails: {_thumbnails.Count}, Transcripts: {_transcripts.Count}] {Title}";
    }

    public Video UpdateTitle(string newTitle)
    {
        Title = Guard.Against.NullOrWhiteSpace(newTitle, nameof(newTitle));

        return this;
    }

    public Video UpdateDescription(string? newDescription)
    {
        Description = newDescription;

        return this;
    }

    public Video AddThumbnail(Thumbnail thumbnail)
    {
        _thumbnails.Add(Guard.Against.Null(thumbnail, nameof(thumbnail)));

        return this;
    }

    public Video AddTranscript(Transcript transcript)
    {
        _transcripts.Add(Guard.Against.Null(transcript, nameof(transcript)));

        return this;
    }

    public Video AddArtifact(Artifact artifact)
    {
        _artifacts.Add(artifact);

        return this;
    }

    public Video ClearTranscripts()
    {
        _transcripts.Clear();

        return this;
    }

    public Video ClearThumbnails()
    {
        _thumbnails.Clear();

        return this;
    }

    public Video ClearArtifacts()
    {
        _artifacts.Clear();
        return this;
    }

    #endregion

    #region Private

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    private Video()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    [JsonProperty(PropertyName = nameof(Collections))]
    readonly internal List<Playlist> _collections = new List<Playlist>();

    [JsonProperty(PropertyName = nameof(Tags))]
    readonly internal List<Tag> _tags = new List<Tag>();

    [JsonProperty(PropertyName = nameof(Artifacts))]
    readonly internal List<Artifact> _artifacts = new List<Artifact>();

    [JsonProperty(PropertyName = nameof(Transcripts))]
    readonly internal List<Transcript> _transcripts = new List<Transcript>();

    [JsonProperty(PropertyName = nameof(Thumbnails))]
    readonly internal List<Thumbnail> _thumbnails = new List<Thumbnail>();

    #endregion
}
