using System.Linq.Expressions;

namespace Ardalis.Specification;

public static class ArdalisOrderByExtensions
{
    public static void OrderBy<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        string[]? orderByFields) 
        where T : class
    {
        if ((orderByFields == null) || (orderByFields.Length == 0))
        {
            if (!typeof(IEntity).IsAssignableFrom(typeof(T)))
                return;
            
            orderByFields = new[] { nameof(IEntity.Id) };            
        }

        var join = string.Join(",", orderByFields); 
        OrderBy(specificationBuilder, join);
    }
    
    static OrderedSpecificationBuilder<T> OrderBy<T>(
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

    static IDictionary<string, OrderTypeEnum> ParseOrderBy(string orderByFields)
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