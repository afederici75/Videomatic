using System.Linq.Expressions;

namespace System.Linq;

public static class IQueryableExtensions
{
    const int DefaultPage = 1;
    const int DefaultPageSize = 10;

    public static IQueryable<T> OrderBy<T>(
        this IQueryable<T> source, 
        string? orderByText,
        IReadOnlyDictionary<string, Expression<Func<T, object?>>> sortExpressions)
    {
        if (string.IsNullOrWhiteSpace(orderByText)) 
        {
            return source;
        }

        IOrderedQueryable<T>? orderedQueryable = null;
        var options = orderByText.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var sortOption in options)
        {
            var parts = sortOption.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var desc = parts.Length > 1 ? parts[1].ToLower().Equals("desc") : false;
            if (!sortExpressions.TryGetValue(parts[0], out var sortExpr))
                throw new Exception($"Cannot sort by '{sortOption}'.");

            if (orderedQueryable == null)
            {
                if (desc)
                    orderedQueryable = source.OrderByDescending(sortExpr);
                else
                    orderedQueryable = source.OrderBy(sortExpr);
            }
            else
            {
                if (desc)
                    orderedQueryable = orderedQueryable.ThenByDescending(sortExpr);
                else
                    orderedQueryable = orderedQueryable.ThenBy(sortExpr);
            }
        }

        return orderedQueryable ?? source;
    }

    public static async Task<PageResult<TDTO>> ToPageAsync<TDTO>(
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

        return new PageResult<TDTO>(
            items,
            page,
            pageSize,
            totalCount);
    }
}
