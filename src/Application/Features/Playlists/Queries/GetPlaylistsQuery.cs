namespace Application.Features.Playlists.Queries;

public class GetPlaylistsQuery(
    string? searchText = null,
    string? orderBy = null,
    int? skip = null,
    int? take = null,
    TextSearchType? searchType = null,
    // 
    IEnumerable<PlaylistId>? playlistIds = null,
    string? searchOn = null) : IRequest<Page<PlaylistDTO>>, IBasicQuery
{ 
    public string? SearchText { get; } = searchText;
    public string? OrderBy { get; } = orderBy;
    public int? Skip { get; } = skip;
    public int? Take { get; } = take;
    public TextSearchType? SearchType { get; } = searchType;
    public IEnumerable<PlaylistId>? PlaylistIds { get; } = playlistIds;
    public string? SearchOn { get; } = searchOn;

    internal class GetPlaylistsQueryValidator : AbstractValidator<GetPlaylistsQuery>
    {
        public GetPlaylistsQueryValidator()
        {
            //When(x => x.SearchText is not null, () =>
            //{
            //    RuleFor(x => x.SearchText).NotEmpty();
            //});

            When(x => x.OrderBy is not null, () =>
            {
                RuleFor(x => x.OrderBy).NotEmpty();
            });

            When(x => x.PlaylistIds is not null, () =>
            {
                RuleFor(x => x.PlaylistIds).NotEmpty();
            });

            RuleFor(x => x.Take).GreaterThan(0);
            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
        }
    }
}

