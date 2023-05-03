namespace Company.SharedKernel.Abstractions;

public interface IReadOnlyRepository<T> 
    where T : class, IEntity
{
    Task<T?> GetByIdAsync(int id, 
        IEnumerable<string>? includes = default,
        CancellationToken cancellationToken = default);

    Task<List<T>> ListAsync(
        ISpecification<T> specification,
        CancellationToken cancellationToken = default);
}
