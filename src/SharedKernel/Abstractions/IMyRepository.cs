using Ardalis.Specification;

namespace SharedKernel.Abstractions;

/// <summary>
/// A wrapper for the interface <see cref="Ardalis.Specification.IRepositoryBase{T}"/>: <inheritdoc/>
/// </summary>
public interface IMyRepository<T> /*: Ardalis.Specification.IRepositoryBase<T>*/ where T : class
{
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
    
    Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default);
    Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    //Task<int> SaveChangesAsync(TDbContext dbContext, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);    
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);


    Task<int> CountAsync(CancellationToken cancellationToken = default);
}
