namespace Company.Videomatic.Application.Features.Playlists.Queries;


public record GetPlaylistsQuery(
    string? SearchText = null,
    string? OrderBy = null,
    int? Page = null,
    int? PageSize = null,
    bool IncludeCounts = false) : IRequest<PageResult<PlaylistDTO>>;

internal class GetPlaylistsQueryValidator : AbstractValidator<GetPlaylistsQuery>
{
    public GetPlaylistsQueryValidator()
    {
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
