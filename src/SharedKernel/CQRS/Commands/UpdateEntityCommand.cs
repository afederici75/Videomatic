using SharedKernel.Abstractions;

namespace SharedKernel.CQRS.Commands;

public record UpdateEntityCommand<TEntity>(int Id) : IRequest<Result<TEntity>>, IRequestWithId
    where TEntity : class;