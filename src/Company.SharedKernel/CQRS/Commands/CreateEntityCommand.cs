using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.CQRS.Commands;

public abstract record CreateEntityCommand<TAggregateRoot>() : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IEntity;