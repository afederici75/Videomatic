namespace Company.Videomatic.Domain.Entities;

public record ArtifactId(long Value = 0)
{
    public static implicit operator long(ArtifactId x) => x.Value;
    public static implicit operator ArtifactId(long x) => new ArtifactId(x);
}

public class Artifact
{
    internal static Artifact Create(VideoId videoId, string title, string type, string? text = null)
    {
        return new Artifact
        {
            VideoId = videoId,
            Title = title,
            Type = type,
            Text = text,
        };
    }

    public ArtifactId Id { get; private set; } = default!;
    public VideoId VideoId { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string Type { get; private set; } = default!;
    public string? Text { get; private set; }

    #region Private

    private Artifact()
    { }

    #endregion
}

