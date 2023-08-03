using Ardalis.Specification;
using System.Linq.Expressions;

namespace System.Linq;

public static class IQueryableExtensions
{
    const int DefaultPage = 1;
    const int DefaultPageSize = 10;

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

        var methodCallExpression = (sortMethod.Body as MethodCallExpression);
        if (methodCallExpression == null)
            throw new Exception("MethodCallExpression null");

        var method = methodCallExpression.Method.GetGenericMethodDefinition();
        var genericSortMethod = method.MakeGenericMethod(typeof(TEntity), prop.Type);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        return (IOrderedQueryable<TEntity>)genericSortMethod.Invoke(query, new object[] { query, sortLambda });
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    }

    public static async Task<Page<TDTO>> ToPageAsync<TDTO>(
        this IQueryable<TDTO> source,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new Page<TDTO>(
            items,
            page,
            pageSize,
            totalCount);
    }
}
