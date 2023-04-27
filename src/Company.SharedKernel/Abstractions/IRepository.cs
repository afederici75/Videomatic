namespace Company.SharedKernel.Abstractions;

/// <inheritdoc/>
public interface IRepository<T> : IRepositoryBase<T> where T : class, IEntity
{
}