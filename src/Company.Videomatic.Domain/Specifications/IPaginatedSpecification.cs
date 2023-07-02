using Ardalis.Specification;

namespace Company.Videomatic.Domain.Abstractions;

public interface IPaginatedSpecification<T> : ISpecification<T>
{
    int Page { get; }
    int PageSize { get; }
}
