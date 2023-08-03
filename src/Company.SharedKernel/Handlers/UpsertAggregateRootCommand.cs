namespace Company.SharedKernel.Abstractions;

public record UpsertAggregateRootCommand<TAggregateRoot>(long? Id) : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot;
