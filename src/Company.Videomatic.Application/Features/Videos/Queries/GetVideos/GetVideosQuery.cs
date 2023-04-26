using Company.Videomatic.Application.Model.Query;
using System.Diagnostics;

namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;


public partial class GetVideosQuery : IQuerySettings, IRequest<IEnumerable<VideoDTO>>
{
    public GetVideosQuery(
        FilterSettings? filter,
        PaginationSettings? pagination,
        OrderOption[] order)
    {
        Filter = filter;
        Pagination = pagination;
        Order = order;
    }

    public FilterSettings? Filter { get; set; }
    public PaginationSettings? Pagination { get; set; }
    public OrderOption[]? Order { get; set; }
}
