using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.SharedKernel.Extensions;
public static class OrderByExtensions
{
    public static OrderedSpecificationBuilder<T> OrderBy<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        string[]? orderByFields)
    {
        var join = string.Join(",", orderByFields ?? new[] { nameof(IEntity.Id) }); 
        return OrderBy(specificationBuilder, join);
    }
    
    public static OrderedSpecificationBuilder<T> OrderBy<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        string orderByFields)
    {
        if (ParseOrderBy(orderByFields) is { } fields)
        {
            foreach (var field in fields)
            {
                var matchedProperty = typeof(T).GetProperties().FirstOrDefault(p => p.Name.ToLower() == field.Key.ToLower());
                if (matchedProperty == null)
                    throw new ArgumentNullException(paramName: field.Key);

                var paramExpr = Expression.Parameter(typeof(T));
                var propAccess = Expression.PropertyOrField(paramExpr, matchedProperty.Name);
                var propAccessConverted = Expression.Convert(propAccess, typeof(object));
                var expr = Expression.Lambda<Func<T, object?>>(propAccessConverted, paramExpr);

                ((List<OrderExpressionInfo<T>>)specificationBuilder.Specification.OrderExpressions)
                    .Add(new OrderExpressionInfo<T>(expr, field.Value));
            }
        }
        var orderedSpecificationBuilder = new OrderedSpecificationBuilder<T>(specificationBuilder.Specification);

        return orderedSpecificationBuilder;
    }

    private static IDictionary<string, OrderTypeEnum> ParseOrderBy(string orderByFields)
    {
        var result = new Dictionary<string, OrderTypeEnum>();
        var fields = orderByFields.Split(',');
        for (var index = 0; index < fields.Length; index++)
        {
            var fieldAndOrder = fields[index].Split(' ');
            var field = fieldAndOrder[0];
            var order = fieldAndOrder.Length > 1 ? fieldAndOrder[1] : string.Empty;
            var orderBy = order.ToUpper() == "DESC" ? OrderTypeEnum.OrderByDescending : OrderTypeEnum.OrderBy;
            if (index > 0)
            {
                orderBy = order.ToUpper() == "DESC" ? OrderTypeEnum.ThenByDescending : OrderTypeEnum.ThenBy;
            }
            result.Add(field.Trim(), value: orderBy);
        }
        return result;
    }
}