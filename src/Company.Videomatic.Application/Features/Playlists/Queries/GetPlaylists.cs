using Company.Videomatic.Application.Features.DataAccess;

namespace Company.Videomatic.Application.Features.Playlists.Queries;

public record GetPlaylistsQuery(
    Filter? Filter = null,
    OrderBy? OrderBy = null,
    Paging? Paging = null,
    bool IncludeCounts = false) : IRequest<PageResult<PlaylistDTO>>
{
    public GetPlaylistsQuery New(params long[] Ids) => new(new Filter(Ids: Ids));
}

internal class GetPlaylistsQueryValidator : AbstractValidator<GetPlaylistsQuery>
{
    public GetPlaylistsQueryValidator()
    {
        RuleFor(x => x.Filter).SetValidator(new FilterValidator());
        RuleFor(x => x.OrderBy).SetValidator(new OrderByValidator());
        RuleFor(x => x.Paging).SetValidator(new PagingValidator());        
    }
}