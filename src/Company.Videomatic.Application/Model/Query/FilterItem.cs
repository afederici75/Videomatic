namespace Company.Videomatic.Application.Model.Query;

public record FilterItem(
    string PropertyName,
    string Value,
    FilterOperator Operator = FilterOperator.Equals);
