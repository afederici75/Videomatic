using Ardalis.Specification;

namespace Company.Videomatic.Domain.Abstractions;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
    //public Task<int> DeleteByIdAsync<TId>(TId id, CancellationToken cancellationToken = default);
}
