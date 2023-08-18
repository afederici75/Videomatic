namespace SharedKernel.Model;

/// <summary>
/// The result of a query that returns paginated results.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="Items">The items that compose the page.</param>
/// <param name="Skip">The index of the page.</param>
/// <param name="Take">The size of the page.</param> 
/// <param name="TotalCount">The total amount of items.</param>
public class Page<T>(IEnumerable<T> items, int skip, int take, long totalCount)
{
    public static Page<T> Empty => new(Enumerable.Empty<T>(), 1, 10, 0);    

    public IEnumerable<T> Items { get; } = items;
    public int Skip { get; } = skip;
    public int Take { get; } = take;
    public long TotalCount { get; } = totalCount;

    
    public int Count => Items.Count();
}