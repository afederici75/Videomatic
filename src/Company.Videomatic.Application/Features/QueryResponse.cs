namespace Company.Videomatic.Application.Features;

public record QueryResponse<TDTO>(IEnumerable<TDTO> Items)
    where TDTO : class
{
    public int Count => Items?.Count() ?? 0;
}
