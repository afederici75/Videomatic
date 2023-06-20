namespace Company.Videomatic.Application.Abstractions
{
    public interface IPagedList<T>
    {
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        IEnumerable<T> Items { get; }
        int Page { get; }
        int PageSize { get; }
        long TotalCount { get; }
    }
}