namespace Company.SharedKernel.Abstractions;

public record DeleteAggregateRootCommand<TAggregateRoot>(int Id) : IRequest<Result<bool>>
    where TAggregateRoot : class, IAggregateRoot;