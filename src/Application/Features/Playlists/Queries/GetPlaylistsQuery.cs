namespace Application.Features.Playlists.Queries;

public class GetPlaylistsQuery(
    string? SearchText = null,
    string? OrderBy = null,
    int? Skip = null,
    int? Take = null,
    TextSearchType? SearchType = null,
    // 
    IEnumerable<PlaylistId>? PlaylistIds = null,
    string? SearchOn = null) : IRequest<Page<PlaylistDTO>>, IBasicQuery
{ 
    public string? SearchText { get; } = SearchText;
    public string? OrderBy { get; } = OrderBy;
    public int? Skip { get; } = Skip;
    public int? Take { get; } = Take;
    public TextSearchType? SearchType { get; } = SearchType;
    public IEnumerable<PlaylistId>? PlaylistIds { get; } = PlaylistIds;
    public string? SearchOn { get; } = SearchOn;

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

