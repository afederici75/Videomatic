namespace Company.SharedKernel.Queries;

public record QueryResponse<TDTO>(int Count, IEnumerable<TDTO> Items)
    where TDTO: class;
