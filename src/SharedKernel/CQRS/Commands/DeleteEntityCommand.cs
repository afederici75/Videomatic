using SharedKernel.Abstractions;

namespace SharedKernel.CQRS.Commands;

public record DeleteEntityCommand<TAggregateRoot>(int Id) : IRequest<Result<bool>>, IRequestWithId
    where TAggregateRoot : class;