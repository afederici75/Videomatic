using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.Common.CQRS;

public abstract record CreateAggregateRootCommand<TAggregateRoot>() : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot;