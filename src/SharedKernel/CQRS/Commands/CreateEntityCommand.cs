namespace SharedKernel.CQRS.Commands;

public abstract record CreateEntityCommand<TAggregateRoot>() : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class;