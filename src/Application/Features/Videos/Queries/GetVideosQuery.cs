namespace Application.Features.Videos.Queries;

public class GetVideosQuery(
    string? searchText = null,
    string? orderBy = null,
    int? skip = null,
    int? take = null,
    TextSearchType? searchType = null,
    // Additional
    IEnumerable<int>? playlistIds = null,
    IEnumerable<int>? videoIds = null,
    string? searchOn = null) : IRequest<Page<VideoDTO>>, IBasicQuery
{
    public string? SearchText { get; } = searchText;
    public string? OrderBy { get; } = orderBy;
    public int? Skip { get; } = skip;
    public int? Take { get; } = take;
    public TextSearchType? SearchType { get; } = searchType;
    // Additional
    public IEnumerable<int>? PlaylistIds { get; } = playlistIds;
    public IEnumerable<int>? VideoIds { get; } = videoIds;
    public string? SearchOn { get; } = searchOn;


    internal class GetVideosQueryValidator : AbstractValidator<GetVideosQuery>
    {
        public GetVideosQueryValidator()
        {
            When(x => x.PlaylistIds is not null, () =>
            {
                RuleFor(x => x.PlaylistIds).NotEmpty();
            });

            When(x => x.VideoIds is not null, () =>
            {
                RuleFor(x => x.VideoIds).NotEmpty();
            });

            When(x => x.SearchText is not null, () =>
            {
                // 
                //RuleFor(x => x.SearchText).NotEmpty();
            });

            When(x => x.OrderBy is not null, () =>
            {
                RuleFor(x => x.OrderBy).NotEmpty();
            });

            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Take).GreaterThan(0);
        }
    }
}