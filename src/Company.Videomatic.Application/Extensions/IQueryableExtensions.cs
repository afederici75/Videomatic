using Company.Videomatic.Application.Features.Videos.Queries.GetVideos;
using Company.Videomatic.Application.Model.Query;

namespace Company.Videomatic.Application;

public static class IQueryableExtensions
{
    public static IQueryable<T> ApplySettings<T>(this IQueryable<T> source,
        IQuerySettings settings)
    {
        if (settings.Filter != null)
        {
            source = source.ApplyFilter(settings.Filter);
        }

        if (settings.Order != null)
        {
            source = source.ApplyOrder(settings.Order);
        }

        if (settings.Pagination != null)
        {
            source = source.ApplyPagination(settings.Pagination);
        }

        return source;
    }

    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> source,
        FilterSettings settings)
    {
        return source;
    }

    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> source,
        PaginationSettings settings)
    {
        source = source.Skip(settings.Skip ?? 0);
        source = source.Take(settings.Take ?? 10);

        return source;
    }

    public static IQueryable<T> ApplyOrder<T>(
        this IQueryable<T> source,
        OrderOption[] options)
    {
        foreach (var option in options)
        {
            //source = source.OrderBy()
            //source = source.ApplyOrder(option);
        }
        return source;
    }
}