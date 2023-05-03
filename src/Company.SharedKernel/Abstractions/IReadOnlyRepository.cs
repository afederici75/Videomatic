namespace Company.SharedKernel.Abstractions;

public interface IReadOnlyRepository<T> where T : class
{
    Task<T?> GetByIdAsync<TId>(TId id, 
        IEnumerable<string>? includes = default,
        IEnumerable<string>? order = default,
        CancellationToken cancellationToken = default)
        where TId : notnull;

    Task<List<T>> ListAsync(
        ISpecification<T> specification,
        CancellationToken cancellationToken = default);
}
