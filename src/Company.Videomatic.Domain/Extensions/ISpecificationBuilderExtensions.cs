using Ardalis.Specification;
using Company.Videomatic.Domain.Abstractions;
using System.Linq.Expressions;

namespace Company.Videomatic.Domain.Extensions;

public static class ISpecificationBuilderExtensions
{
    public static ISpecificationBuilder<T> OrderByExpressions<T>(
        this ISpecificationBuilder<T> source,
        string? orderByText,
        IReadOnlyDictionary<string, Expression<Func<T, object?>>> validExpressions)
    {
        if (string.IsNullOrWhiteSpace(orderByText))
        {
            return source;
        }

        IOrderedSpecificationBuilder<T>? orderedQueryable = null;
        var options = orderByText.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var sortOption in options)
        {
            var parts = sortOption.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            
            var desc = parts.Length > 1 && parts[1].ToLower().Equals("desc");

            if (!validExpressions.TryGetValue(parts[0], out var sortExpr))
                throw new Exception($"Cannot sort by '{sortOption}'.");

            if (orderedQueryable == null)
            {
                var x = source.OrderByDescending(sortExpr);

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

        return source;
    }

}