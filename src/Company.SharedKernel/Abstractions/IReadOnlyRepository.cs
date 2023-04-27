namespace Company.SharedKernel.Abstractions;

/// <inheritdoc/>
public interface IReadOnlyRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}