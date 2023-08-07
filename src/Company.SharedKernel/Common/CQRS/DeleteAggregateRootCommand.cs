using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.Common.CQRS;

public record DeleteAggregateRootCommand<TAggregateRoot>(int Id) : IRequest<Result<bool>>
    where TAggregateRoot : class, IAggregateRoot;