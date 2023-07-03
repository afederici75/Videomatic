using Company.Videomatic.Domain.Abstractions;

namespace Company.Videomatic.Application.Features.Artifact.Queries;

public record GetArtifactsQuery(string? SearchText = null,
    string? OrderBy = null,
    int? Page = null,
    int? PageSize = null,
    bool IncludeCounts = false) : IRequest<PageResult<ArtifactDTO>>;



