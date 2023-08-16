using SharedKernel.Abstractions;

namespace SharedKernel.CQRS.Commands;

public record DeleteEntityCommand<TEntity>(int Id) : IRequest<Result>, IRequestWithId
    where TEntity : class;