using Company.Videomatic.Application.Query;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace System.Linq;

public static class IQueryableExtensions
{
    const int DefaultPage = 1;
    const int DefaultPageSize = 10;

    public static async Task<PageResult<TDTO>> ToPageAsync<TPROJECTION, TDTO>(
        this IQueryable<TPROJECTION> source,
        Paging? paging,
        Func<TPROJECTION, TDTO> func,
        CancellationToken cancellationToken = default)
        => await source.ToPageAsync(paging?.Page ?? DefaultPage, paging?.PageSize ?? DefaultPageSize, func, cancellationToken);    

    public static async Task<PageResult<TDTO>> ToPageAsync<TPROJECTION, TDTO>(
        this IQueryable<TPROJECTION> source,
        int page,
        int pageSize,
        Func<TPROJECTION, TDTO> func,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var xxx = items.Select(func).ToList();

        return new PageResult<TDTO>(
            xxx,
            page,
            pageSize,
            totalCount);
    }

    public static async Task<PageResult<TDTO>> ToPageAsync<TDTO>(
        this IQueryable<TDTO> source,
        Paging? options,
        CancellationToken cancellationToken = default)
        => await source.ToPageAsync(options?.Page ?? DefaultPage, options?.PageSize ?? DefaultPageSize, cancellationToken);

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

    public static IQueryable<T> ApplyOrderBy<T>(this IQueryable<T> source, OrderBy? options)
    {
        if (options == null)
        {
            return source;
        }

        OrderByItem[] list = options.Items;
        if (list.Length == 0)
        {
            if (source is not IEntity)
                throw new Exception($"OrderBy cannot be inferred on type {typeof(T)}.");

            list = new[] { new OrderByItem(nameof(IEntity.Id)) };
        }

        // See 'Ordering Results' at https://dynamic-linq.net/basic-simple-query
        var textClauses = list.Select(
            item => $"{item.Property} {(item.Direction == OrderDirection.Asc ? "ASC" : "DESC")}");

        var fullOrderBy = string.Join(", ", textClauses);

        return source.OrderBy(fullOrderBy);
    }

    static string CreateClause(FilterItem filter, int index)
    {
        switch (filter.Type)
        {
            case FilterType.Equals:
                return $"({filter.Property} == @{index})";
            case FilterType.Contains:
                return $"({filter.Property}.Contains(@{index}))";
            case FilterType.StartsWith:
                return $"({filter.Property}.StartsWith(@{index}))";
            case FilterType.EndsWith:
                return $"({filter.Property}.EndsWith(@{index}))";
            case FilterType.GreaterThan:
                return $"({filter.Property} > @{index})";
            case FilterType.GreaterThanOrEqual:
                return $"({filter.Property} >= @{index})";
            case FilterType.LessThan:
                return $"({filter.Property} < @{index})";
            case FilterType.LessThanOrEqual:
                return $"({filter.Property} <= @{index})";
            case FilterType.NotEqual:
                return $"({filter.Property} <> @{index})";
        }

        throw new NotSupportedException();
    }

    public static IQueryable<T> ApplyFilters<T>(
        this IQueryable<T> source,
        Filter? options,
        string[] searchTextProperties)
    {
        if (options is null)
        {
            return source;
        }

        if (options.Ids is not null)
        {
            source = source.Where("Id in @0", options.Ids);
        }

        if (!string.IsNullOrWhiteSpace(options.SearchText))
        {
            // Hux -> Title.Contains('HUX') || Description.Contains('HUX')
            var x1 = searchTextProperties.Select(propName => $"{propName}.Contains(@0)");
            var allTextClauses = '(' + string.Join(" || ", x1) + ')';

            source = source.Where(allTextClauses, options.SearchText);
        }

        if (options.Items?.Length > 0)
        {
            var idx = 0;
            var items = options.Items.Select(x => new
            {
                Clause = CreateClause(x, idx++),
                x.Value
            });

            var finalText = $"({string.Join(" && ", items.Select(x => x.Clause))})";
            var values = items.Select(x => x.Value).ToArray();

            source = source.Where(finalText, values);
        }

        return source;
    }
}
