namespace Company.Videomatic.Application.Features.Artifacts.Queries;

public record GetArtifactsQuery(
    string? SearchText = null,
    string? OrderBy = null,
    int? Page = null,
    int? PageSize = null,
    IEnumerable<long>? ArtifactIds = null) : IRequest<PageResult<ArtifactDTO>>;

internal class GetArtifactsQueryValidator : AbstractValidator<GetArtifactsQuery>
{
    public GetArtifactsQueryValidator()
    {
        When(x => x.SearchText is not null, () =>
        {
            RuleFor(x => x.SearchText).NotEmpty();
        });

        When(x => x.ArtifactIds is not null, () =>
        {
            RuleFor(x => x.ArtifactIds).NotEmpty();
        });

        When(x => x.OrderBy is not null, () =>
        {
            RuleFor(x => x.OrderBy).NotEmpty();
        });

        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}

