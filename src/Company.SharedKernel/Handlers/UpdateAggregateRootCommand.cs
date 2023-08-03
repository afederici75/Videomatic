namespace Company.SharedKernel.Abstractions;

public record UpdateAggregateRootCommand<TAggregateRoot>(long Id) : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot;