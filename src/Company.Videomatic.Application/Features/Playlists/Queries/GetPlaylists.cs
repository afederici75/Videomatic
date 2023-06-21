namespace Company.Videomatic.Application.Features.Playlists.Queries;

public record GetPlaylistsQuery(
    Filter? Filter = null,
    OrderBy? OrderBy = null,
    Paging? Paging = null,
    bool IncludeCounts = false) : IRequest<PageResult<PlaylistDTO>>;

public class GetPlaylistsQueryValidator : AbstractValidator<GetPlaylistsQuery>
{
    public GetPlaylistsQueryValidator()
    {
        //RuleFor(x => x.Page).InclusiveBetween(1, 50);
        //RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
    }
}