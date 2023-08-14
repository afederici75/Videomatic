namespace Application.Features.Playlists.Queries;

public record GetPlaylistsQuery(
    // IBasicQuery
    string? SearchText = null,
    string? OrderBy = null,
    int? Skip = null,
    int? Take = null,
    TextSearchType? SearchType = null,
    // Additional
    IEnumerable<int>? PlaylistIds = null,
    string? SearchOn = null) : IRequest<Page<PlaylistDTO>>, IBasicQuery;

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