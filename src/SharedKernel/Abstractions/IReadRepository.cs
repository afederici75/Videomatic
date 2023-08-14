namespace SharedKernel.Abstractions;

/// <summary>
/// A wrapper for the interface <see cref="Ardalis.Specification.IReadRepositoryBase{T}"/>: <inheritdoc/>
/// </summary>
public interface IReadRepository<T> : Ardalis.Specification.IReadRepositoryBase<T> where T : class, IEntity
{
}