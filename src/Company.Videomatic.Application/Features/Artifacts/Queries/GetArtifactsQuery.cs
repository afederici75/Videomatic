namespace Company.Videomatic.Application.Features.Artifacts.Queries;

public record GetArtifactsQuery(
    // IBasicQuery
    string? SearchText = null,
    string? OrderBy = null,
    int? Skip = null,
    int? Take = null,
    // Additional
    IEnumerable<long>? VideoIds = null,
    IEnumerable<long>? ArtifactIds = null) : IRequest<Page<ArtifactDTO>>, IBasicQuery;

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

        When(x => x.VideoIds is not null, () =>
        {
            RuleFor(x => x.VideoIds).NotEmpty();
        });

        When(x => x.OrderBy is not null, () =>
        {
            RuleFor(x => x.OrderBy).NotEmpty();
        });

        RuleFor(x => x.Skip).GreaterThan(0);
        RuleFor(x => x.Take).GreaterThan(0);
    }
}

