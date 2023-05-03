namespace Company.SharedKernel.Abstractions;

public interface IRepository<T> : IReadOnlyRepository<T>
    where T : class, IEntity
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> AddRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default);

    Task UpdateRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default);

    Task DeleteRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default);
}