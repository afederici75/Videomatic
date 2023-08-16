namespace Application.Features.Artifacts;

public class ArtifactDTO(
    int artifactId = 0,
    int videoId = 0,
    string name = "",
    string type = "",
    string? text = null)
{
    public int ArtifactId { get; } = artifactId;
    public int VideoId { get; } = videoId;
    public string Name { get; } = name;
    public string Type { get; } = type;
    public string? Text { get; } = text;
}