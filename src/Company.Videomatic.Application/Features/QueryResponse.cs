namespace Company.Videomatic.Application.Features.Queries;

public record QueryResponse<TDTO>(IEnumerable<TDTO> Items)
    where TDTO : class
{
    public int Count => Items?.Count() ?? 0;
}
