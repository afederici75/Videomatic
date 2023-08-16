namespace SharedKernel.CQRS.Commands;

public abstract record CreateEntityCommand<TEntity>() : IRequest<Result<TEntity>>
    where TEntity : class;