namespace Company.Videomatic.Application.Model.Query;

public record OrderOption(
    string PropertyName,
    OrderDirection Direction = OrderDirection.Asc);
