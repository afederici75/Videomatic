namespace Application.Features.Artifacts;

public record ArtifactDTO( 
    int ArtifactId = 0,
    int VideoId = 0,
    string Name = "",
    string Type = "",
    string? Text = null);
