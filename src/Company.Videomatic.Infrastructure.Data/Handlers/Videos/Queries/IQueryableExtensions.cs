using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Application.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TextTemplating;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Queries;

public static class IQueryableExtensions
{
    public static async Task<PageResult<TDTO>> ToPageAsync<TPROJECTION, TDTO>(
        this IQueryable<TPROJECTION> source, 
        Paging options, 
        Func<TPROJECTION, TDTO> func,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);
        var items = await source
            .Skip((options.Page - 1) * options.PageSize)
            .Take(options.PageSize)
            .ToListAsync(cancellationToken);

        return new PageResult<TDTO>(items.Select(func), options.Page, options.PageSize, totalCount);

        //return new PageResult<TPROJECTION>(items, options.Page, options.PageSize, totalCount);
    }

    public static IQueryable<T> ApplyOrderBy<T>(this IQueryable<T> source, OrderBy options)        
    {
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
        Filter options,
        string[] searchTextProperties)
    {
        if (options.Ids is not null)
        {            
            source = source.Where("Id in @0", options.Ids);
        }

        if (options.SearchText != null)
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
                Value = x.Value
            });

            var finalText = $"({string.Join(" && ", items.Select(x => x.Clause))})";
            var values = items.Select(x => x.Value).ToArray();
            
            source = source.Where(finalText, values);
        }

        return source;
    }
}
