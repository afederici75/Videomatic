using AutoMapper.Execution;
using System.Linq.Expressions;

namespace Company.Videomatic.Application.Query;

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

public class OrderByItemValidator : AbstractValidator<OrderByItem>
{
    public OrderByItemValidator()
    {
        RuleFor(x => x.Property).NotEmpty();
        RuleFor(x => x.Direction).IsInEnum();
    }
}