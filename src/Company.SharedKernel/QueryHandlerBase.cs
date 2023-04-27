namespace Company.SharedKernel;

public abstract class QueryHandlerBase<TSpecification, TEntity> : IQueryHandler<TSpecification, TEntity>
    where TSpecification : class, ISpecification<TEntity>, IRequest<TEntity>
    where TEntity : class
{
    public Task<TEntity> Handle(TSpecification request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
