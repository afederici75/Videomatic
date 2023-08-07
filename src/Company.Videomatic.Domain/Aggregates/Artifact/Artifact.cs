using Company.Videomatic.Domain.Aggregates.Video;

namespace Company.Videomatic.Domain.Aggregates.Artifact;

public class Artifact : IEntity, IAggregateRootx
{
    public static Artifact Create(VideoId videoId, string name, string type, string? text = null)
    {
        return new Artifact // TODO: code smell with the other ctors...
        {
            VideoId = videoId,
            Name = name,
            Type = type,
            Text = text,
        };
    }

    public ArtifactId Id { get; private set; } = default!;
    public VideoId VideoId { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Type { get; private set; } = default!;
    public string? Text { get; private set; }
    
    #region Private

    private Artifact()
    { }


    [JsonConstructor]
    private Artifact(ArtifactId id, VideoId videoId, string name, string type, string? text)
    {
        Id = id;
        VideoId = videoId;
        Name = name;
        Type = type;
        Text = text;
    }

    int IEntity.Id => this.Id;

    #endregion
}

