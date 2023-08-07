using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.Common.CQRS;

public record DeleteEntityCommand<TAggregateRoot>(int Id) : IRequest<Result<bool>>, IRequestWithId
    where TAggregateRoot : class, IEntity;