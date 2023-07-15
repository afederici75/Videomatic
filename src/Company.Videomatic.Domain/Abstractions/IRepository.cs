using Ardalis.Specification;

namespace Company.Videomatic.Domain.Abstractions;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
