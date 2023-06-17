namespace Company.Videomatic.Domain.Model;

public class Video : EntityBase, IAggregateRoot
{    
    public string Location { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }

    public IReadOnlyCollection<Tag> Tags
    {
        get => _tags.ToImmutableList();
        private set => _tags = value.ToList();
    }

    public IReadOnlyCollection<Playlist> Playlists
    {
        get => _playlists.ToImmutableList();
        private set => _playlists = value.ToList();
    }
    
    public IReadOnlyCollection<Artifact> Artifacts
    {
        get => _artifacts.ToImmutableList();
        private set => _artifacts = value.ToList();
    }

    public IReadOnlyCollection<Thumbnail> Thumbnails
    {
        get => _thumbnails.ToImmutableList();
        private set => _thumbnails = value.ToList();
    }

    public IReadOnlyCollection<Transcript> Transcripts
    {
        get => _transcripts.ToImmutableList();
        private set => _transcripts = value.ToList();
    }

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

    public Video AddTag(Tag tag)
    {
        _tags.Add(Guard.Against.Null(tag, nameof(tag)));

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

    List<Playlist> _playlists = new List<Playlist>();

    List<Tag> _tags = new List<Tag>();

    List<Artifact> _artifacts = new List<Artifact>();

    List<Transcript> _transcripts = new List<Transcript>();

    List<Thumbnail> _thumbnails = new List<Thumbnail>();

    #endregion
}
