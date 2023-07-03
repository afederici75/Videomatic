using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Video;

namespace Company.Videomatic.Application.Features.Artifact;

public record ArtifactDTO(ArtifactId ArtifactId, VideoId VideoId, string Title, string Type, string? Text);
