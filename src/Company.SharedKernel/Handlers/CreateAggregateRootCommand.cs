namespace Company.SharedKernel.Abstractions;

public abstract record CreateAggregateRootCommand<TAggregateRoot>() : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot;