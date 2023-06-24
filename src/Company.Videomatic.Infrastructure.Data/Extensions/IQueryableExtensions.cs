using Company.Videomatic.Application.Features.DataAccess;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace System.Linq;

public static class IQueryableExtensions
{
    const int DefaultPage = 1;
    const int DefaultPageSize = 10;

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
