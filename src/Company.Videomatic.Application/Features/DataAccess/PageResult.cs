namespace Company.Videomatic.Application.Features.DataAccess;

public record PageResult<T>(IEnumerable<T> Items, int Page, int PageSize, long TotalCount)
{
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
    public int Count => Items.Count();
}
