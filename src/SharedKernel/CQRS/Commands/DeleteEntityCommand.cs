using SharedKernel.Abstractions;

namespace SharedKernel.CQRS.Commands;

public record DeleteEntityCommand<TAggregateRoot>(int Id) : IRequest<Result>, IRequestWithId
    where TAggregateRoot : class;