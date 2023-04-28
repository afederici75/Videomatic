namespace Company.SharedKernel.Queries;

public record QueryResponse<TDTO>(IEnumerable<TDTO> Items)
    where TDTO : class
{
    public int Count => Items?.Count() ?? 0;
}
