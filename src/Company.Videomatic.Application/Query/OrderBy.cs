using System.Linq.Expressions;

namespace Company.Videomatic.Application.Query;

public record OrderBy(params OrderByItem[] Items)
{
}

public enum OrderDirection
{
    Asc,
    Desc
}

public record OrderByItem(
    string Property,
    OrderDirection Direction = OrderDirection.Asc)
{    
}