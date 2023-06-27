using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace System.Linq;

public static class IQueryableExtensions
{
    const int DefaultPage = 1;
    const int DefaultPageSize = 10;

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderByText)
    {
        var items = orderByText.Split(
            new char[] { ',' }, 
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        foreach (var item in items)
        {
            var parts = item.Split();
            var desc = parts.Length > 1 ? parts[1].ToLower().Equals("desc") : false;
            if (desc)
                source = source.OrderByDescending(x => EF.Property<object>(x!, parts[0]));
            else
                source = source.OrderBy(x => EF.Property<object>(x!, parts[0]));
        }

        return source;
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
