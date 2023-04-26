using Company.Videomatic.Application.Model.Query;

namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;

public interface IQuerySettings
{
    FilterSettings? Filter { get; }
    PaginationSettings? Pagination { get; }
    OrderOption[]? Order { get; }
}

public record QuerySettings(
    FilterSettings? Filter, 
    PaginationSettings? Pagination, 
    OrderOption[]? Order) : IQuerySettings;
