using System.Linq.Expressions;

namespace Company.Videomatic.Application.Query;

public record FilterItem(
    string Property,
    FilterType Type = FilterType.Equals,
    string? Value = null);

//public record FilterItem<TDTO>(
//    Expression<Func<TDTO, object?>> expr,
//    FilterType Type = FilterType.Equals,
//    string? Value = null) : FilterItem(typeof(TDTO).Name, Type, Value);

public class FilterItemValidator : AbstractValidator<FilterItem>
{
    public FilterItemValidator()
    {
        RuleFor(x => x.Property).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
    }
}