namespace Company.Videomatic.Application.Features.Artifacts.Queries;

public record GetArtifactsByIdQuery(
    long[] ArtifactIds,
    string? SearchText = null,
    string? OrderBy = null,
    int? Page = null,
    int? PageSize = null) : IRequest<IEnumerable<ArtifactDTO>>;

internal class GetArtifactsByIdQueryValidator : AbstractValidator<GetArtifactsByIdQuery>
{
    public GetArtifactsByIdQueryValidator()
    {
        RuleFor(x => x.ArtifactIds).NotEmpty();

        When(x => x.SearchText is not null, () =>
        {
            RuleFor(x => x.SearchText).NotEmpty();
        });

        When(x => x.OrderBy is not null, () =>
        {
            RuleFor(x => x.OrderBy).NotEmpty();
        });

        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}

