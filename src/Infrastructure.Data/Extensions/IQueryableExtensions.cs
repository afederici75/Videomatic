using System.Linq.Expressions;

namespace System.Linq;

// TODO: this stuff might needs some clean up. I don't like the pragmas I had to add to get it to compile without warnings or messages.

public static class IQueryableExtensions
{
    public enum SortDirection
    { 
        Asc,
        Desc
    }

    public static IQueryable<T> OrderBy<T>(
        this IQueryable<T> source, 
        string? orderByText)
    {
        if (string.IsNullOrWhiteSpace(orderByText)) 
        {
            return source;
        }

        var options = orderByText.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var sortOption in options)
        {
            var parts = sortOption.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var propertyName = parts[0];
            var dir = (parts.Length > 1 && parts[1].ToLower().Equals("desc")) ? SortDirection.Desc : SortDirection.Asc;

            source = AddSorting(source, dir, propertyName);
        }

        return source;
    }

    // See https://medium.com/@erickgallani/the-power-of-entity-framework-core-and-linq-expression-tree-combined-6b0d72cf41db
    static IQueryable<TEntity> AddSorting<TEntity>(IQueryable<TEntity> query, SortDirection sortDirection, string propertyName)
    {
        var param = Expression.Parameter(typeof(TEntity));
        var prop = Expression.PropertyOrField(param, propertyName);
        var sortLambda = Expression.Lambda(prop, param);

        Expression<Func<IOrderedQueryable<TEntity>>>? sortMethod = null;

#pragma warning disable CS8603 // Possible null reference return.

#pragma warning disable IDE0066 // Convert switch statement to expression
        switch (sortDirection)
        {
            case SortDirection.Asc when query.Expression.Type == typeof(IOrderedQueryable<TEntity>):
                sortMethod = () => ((IOrderedQueryable<TEntity>)query).ThenBy<TEntity, object>(k => null);
                break;
            default:
            case SortDirection.Asc:
                sortMethod = () => query.OrderBy<TEntity, object>(k => null);
                break;
            case SortDirection.Desc when query.Expression.Type == typeof(IOrderedQueryable<TEntity>):
                sortMethod = () => ((IOrderedQueryable<TEntity>)query).ThenByDescending<TEntity, object>(k => null);
                break;
            case SortDirection.Desc:
                sortMethod = () => query.OrderByDescending<TEntity, object>(k => null);
                break;
        }
#pragma warning restore IDE0066 // Convert switch statement to expression

        var methodCallExpression = (sortMethod.Body as MethodCallExpression) ?? throw new Exception("MethodCallExpression null");
        var method = methodCallExpression.Method.GetGenericMethodDefinition();
        var genericSortMethod = method.MakeGenericMethod(typeof(TEntity), prop.Type);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        return (IOrderedQueryable<TEntity>)genericSortMethod.Invoke(query, new object[] { query, sortLambda });
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

#pragma warning restore CS8603 // Possible null reference return.
    }

    //public static async Task<Page<TDTO>> ToPageAsync<TDTO>(
    //    this IQueryable<TDTO> source,
    //    int page,
    //    int pageSize,
    //    CancellationToken cancellationToken = default)
    //{
    //    var totalCount = await source.CountAsync(cancellationToken);

    //    var items = await source
    //        .Skip((page - 1) * pageSize)
    //        .Take(pageSize)
    //        .ToListAsync(cancellationToken);

    //    return new Page<TDTO>(
    //        items,
    //        page,
    //        pageSize,
    //        totalCount);
    //}
}
