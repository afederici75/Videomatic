using Company.Videomatic.Application.Features.DataAccess;

namespace Company.Videomatic.Application.Features.Playlists.Queries;

public record GetPlaylistsQuery(
    Filter? Filter = null,
    OrderBy? OrderBy = null,
    Paging? Paging = null,
    bool IncludeCounts = false) : IRequest<PageResult<PlaylistDTO>>
{
    public GetPlaylistsQuery(bool IncludeCounts, params long[] Ids) : this(Filter: new Filter(Ids: Ids), OrderBy: null, Paging: null, IncludeCounts) 
    { }
}

public class GetPlaylistsQuery2 
{
    public GetPlaylistsQuery2()
    {
            
    }

    public GetPlaylistsQuery2(Filter filter, OrderBy orderBy, Paging paging, bool includeCounts) =>
        (Filter, OrderBy, Paging, IncludeCounts) = (filter, orderBy, paging, includeCounts);

    public Filter? Filter { get; set; }
    public OrderBy? OrderBy { get; set; }
    public Paging? Paging { get; set; }
    bool IncludeCounts { get; set; }
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