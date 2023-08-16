namespace Application.Features.Artifacts.Queries;

public class GetArtifactsQuery(
    string? searchText = null,
    string? orderBy = null,
    int? skip = null,
    int? take = null,
    TextSearchType? searchType = null,    
    //
    IEnumerable<VideoId>? videoIds = null,
    IEnumerable<ArtifactId>? artifactIds = null,
    string? SearchOn = null) : IRequest<Page<ArtifactDTO>>, IBasicQuery
{ 
    public string? SearchText { get; } = searchText; 
    public string? OrderBy { get; } = orderBy;
    public int? Skip  { get; } = skip;
    public int? Take { get; } = take;
    public TextSearchType? SearchType { get; } = searchType;
    public IEnumerable<VideoId>? VideoIds { get; } = videoIds;
    public IEnumerable<ArtifactId>? ArtifactIds { get; } = artifactIds;
    public string? SearchOn { get; } = SearchOn;

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


}

