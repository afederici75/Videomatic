using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.Common.CQRS;

public record UpsertAggregateRootCommand<TAggregateRoot>(long? Id) : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot;
