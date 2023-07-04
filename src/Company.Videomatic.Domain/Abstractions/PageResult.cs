using System.Collections.Generic;

namespace Company.Videomatic.Domain.Abstractions;

public record PageResult<T>(IEnumerable<T> Items, int Page, int PageSize, long TotalCount)
{
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
    public int Count => Items.Count();
}
