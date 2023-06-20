namespace Company.Videomatic.Infrastructure.Data.Handlers;

public class PagedList<T> : IPagedList<T>
{
    private PagedList(IEnumerable<T> items, int page, int pageSize, long totalCount)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public IEnumerable<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public long TotalCount { get; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;

    public static async Task<IPagedList<T>> CreateAsync(IQueryable<T> items, int page, int pageSize)
    {
        var totalCount = await items.CountAsync();
        return new PagedList<T>(items, page, pageSize, totalCount);
    }
}
