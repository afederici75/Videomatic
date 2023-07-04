using Ardalis.Specification;

namespace Company.Videomatic.Domain.Abstractions;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}