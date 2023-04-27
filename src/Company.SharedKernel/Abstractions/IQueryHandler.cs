namespace Company.SharedKernel.Abstractions;

public interface IQueryHandler<TSpecification, TEntity> : IRequestHandler<TSpecification, TEntity>
    where TSpecification : class, ISpecification<TEntity>, IRequest<TEntity>
    where TEntity : class
{ }
