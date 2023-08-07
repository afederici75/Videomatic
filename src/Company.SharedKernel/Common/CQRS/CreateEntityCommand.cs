using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.Common.CQRS;

public abstract record CreateEntityCommand<TAggregateRoot>() : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IEntity;