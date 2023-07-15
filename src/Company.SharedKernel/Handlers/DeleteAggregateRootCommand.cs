namespace Company.SharedKernel.Abstractions;

public record DeleteAggregateRootCommand<TAggregateRoot>(long Id) : IRequest<Result<bool>>
    where TAggregateRoot : class, IAggregateRoot;