namespace Company.Videomatic.Application.Features.Artifacts;

public record ArtifactDTO( 
    long ArtifactId = 0,
    long VideoId = 0,
    string Title = "",
    string Type = "",
    string? Text = null);
