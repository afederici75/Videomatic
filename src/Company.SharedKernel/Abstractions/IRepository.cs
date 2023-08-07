namespace Company.SharedKernel.Abstractions;

/// <summary>
/// A wrapper for the interface <see cref="Ardalis.Specification.IRepositoryBase{T}"/>: <inheritdoc/>
/// </summary>
public interface IRepository<T> : Ardalis.Specification.IRepositoryBase<T> where T : class, IEntity
{
}
