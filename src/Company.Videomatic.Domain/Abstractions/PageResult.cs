namespace Company.Videomatic.Domain.Abstractions;

public record PageResult<T>(IEnumerable<T> Items, int PageIndex, int PageSize, long TotalCount)
{
    public bool HasNextPage => PageIndex * PageSize < TotalCount;
    public bool HasPreviousPage => PageIndex > 1;
    public int Count => Items.Count();
}
