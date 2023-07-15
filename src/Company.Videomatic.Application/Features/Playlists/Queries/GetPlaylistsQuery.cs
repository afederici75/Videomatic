namespace Company.Videomatic.Application.Features.Playlists.Queries;

public record GetPlaylistsQuery(
    string? SearchText = null,
    string? OrderBy = null,
    int? Page = null,
    int? PageSize = null,
    IEnumerable<long>? PlaylistIds = null) : IRequest<Page<PlaylistDTO>>;

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

        When(x => x.PlaylistIds is not null, () =>
        {
            RuleFor(x => x.PlaylistIds).NotEmpty();
        });

        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}