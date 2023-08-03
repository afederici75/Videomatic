namespace Company.Videomatic.Application.Features.Artifacts;

public record ArtifactDTO( 
    long ArtifactId = 0,
    long VideoId = 0,
    string Name = "",
    string Type = "",
    string? Text = null);
