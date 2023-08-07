using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.Common.CQRS;

public record UpdateAggregateRootCommand<TAggregateRoot>(int Id) : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot;