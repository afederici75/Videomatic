using Domain.Videos;
using SharedKernel.Model;

namespace Domain.Artifacts;

public class Artifact : TrackedEntity, IAggregateRoot
{
    public Artifact(VideoId videoId, string name, string type, string? text = null)
    {
        VideoId = videoId;
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Type = Guard.Against.NullOrWhiteSpace(type);
        Text = text;
    }

    public ArtifactId Id { get; private set; } = default!;
    public VideoId VideoId { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Type { get; private set; } = default!;
    public string? Text { get; private set; }
    
    #region Private

    private Artifact()
    { }

    #endregion
}

