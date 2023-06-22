namespace Company.Videomatic.Application.Features.DataAccess;

/// <summary>
/// A property filter that filters the results of a query by a property name, a filter type and a value.
/// </summary>
/// <param name="Property">The name of the property.</param>
/// <param name="Type">The type of filter.</param>
/// <param name="Value">A text value.</param>
public record FilterItem(
    string Property,
    FilterType Type = FilterType.Equals,
    string? Value = null);

#region Validator

public class FilterItemValidator : AbstractValidator<FilterItem>
{
    // TODO: dubious location for this class
    public static class Lengths
    {
        public const int MaxPropertyLength = 128;
    }
    public FilterItemValidator()
    {
        RuleFor(x => x.Property).Length(1, Lengths.MaxPropertyLength);
        RuleFor(x => x.Type).IsInEnum();
    }
}

#endregion