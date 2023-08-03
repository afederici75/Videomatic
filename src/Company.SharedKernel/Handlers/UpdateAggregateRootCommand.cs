namespace Company.SharedKernel.Abstractions;

public record UpdateAggregateRootCommand<TAggregateRoot>(int Id) : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot;