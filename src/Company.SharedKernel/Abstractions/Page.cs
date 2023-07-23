namespace Company.SharedKernel.Abstractions;

/// <summary>
/// The result of a query that returns paginated results.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="Items">The items that compose the page.</param>
/// <param name="Skip">The index of the page.</param>
/// <param name="Take">The size of the page.</param> 
/// <param name="TotalCount">The total amount of items.</param>
public record Page<T>(IEnumerable<T> Items, int Skip, int Take, long TotalCount)
{
    public static Page<T> Empty => new(Enumerable.Empty<T>(), 1, 10, 0);    

    //public bool HasNextPage => PageIndex * PageSize < TotalCount;
    //public bool HasPreviousPage => PageIndex > 1;
    public int Count => Items.Count();
}