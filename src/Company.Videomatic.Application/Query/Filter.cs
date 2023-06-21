using System.Linq.Expressions;

namespace Company.Videomatic.Application.Query;

public record Filter(
    string? SearchText = null,
    long[]? Ids = null,
    params FilterItem[] Items);

public enum FilterType
{
    Equals,
    Contains,
    StartsWith,
    EndsWith,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    NotEqual
}

public record FilterItem(
    string Property,
    FilterType Type = FilterType.Equals,
    string? Value = null);

public record FilterItem<TDTO>(
    Expression<Func<TDTO, object?>> expr,
    FilterType Type = FilterType.Equals,
    string? Value = null) : FilterItem(typeof(TDTO).Name, Type, Value);