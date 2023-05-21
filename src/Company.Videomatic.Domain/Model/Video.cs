namespace Company.Videomatic.Domain.Model;

public class Video : EntityBase, IAggregateRoot
{    
    public string ProviderId { get; init; }
    public string ProviderVideoId { get; init; }
    public string VideoUrl { get; init; }

    public string? Title { get; set; }
    public string? Description { get; set; }

    [JsonIgnore]
    public IEnumerable<Tag> Tags => _tags.AsReadOnly();

    [JsonIgnore]
    public IEnumerable<Collection> Collections => _collections.AsReadOnly();

    [JsonIgnore]
    public IEnumerable<Artifact> Artifacts => _artifacts.AsReadOnly();

    [JsonIgnore]
    public IEnumerable<Thumbnail> Thumbnails => _thumbnails.AsReadOnly();

    [JsonIgnore]
    public IEnumerable<Transcript> Transcripts => _transcripts.AsReadOnly();

    public Video(string providerId, string providerVideoId, string videoUrl, string? title = null, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(providerId))
        {
            throw new ArgumentException($"'{nameof(providerId)}' cannot be null or whitespace.", nameof(providerId));
        }

        if (string.IsNullOrWhiteSpace(providerVideoId))
        {
            throw new ArgumentException($"'{nameof(providerVideoId)}' cannot be null or whitespace.", nameof(providerVideoId));
        }

        if (string.IsNullOrWhiteSpace(videoUrl))
        {
            throw new ArgumentException($"'{nameof(videoUrl)}' cannot be null or whitespace.", nameof(videoUrl));
        }

        ProviderId = providerId;
        ProviderVideoId = providerVideoId;
        VideoUrl = videoUrl;
        Title = title;
        Description = description;
    }

    public override string ToString()
    {
        return $"[{ProviderVideoId}@{ProviderId}, Thumbnails: {_thumbnails.Count}, Transcripts: {_transcripts.Count}] {Title}";
    }

    public Video AddThumbnails(params Thumbnail[] thumbnails)
    {
        _thumbnails.AddRange(thumbnails);

        return this;
    }

    public Video AddTranscripts(params Transcript[] transcripts)
    {
        _transcripts.AddRange(transcripts);

        return this;
    }

    public Video AddArtifacts(params Artifact[] artifacts)
    {
        _artifacts.AddRange(artifacts);

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

    #region Private

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    private Video()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    [JsonProperty(PropertyName = nameof(Collections))]
    private List<Collection> _collections = new List<Collection>();

    [JsonProperty(PropertyName = nameof(Tags))]
    private List<Tag> _tags = new List<Tag>();

    [JsonProperty(PropertyName = nameof(Artifacts))]
    private List<Artifact> _artifacts = new List<Artifact>();

    [JsonProperty(PropertyName = nameof(Transcripts))]
    private List<Transcript> _transcripts = new List<Transcript>();

    [JsonProperty(PropertyName = nameof(Thumbnails))]
    private List<Thumbnail> _thumbnails = new List<Thumbnail>();

    #endregion
}