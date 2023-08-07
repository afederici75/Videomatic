using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.Common.CQRS;

public record UpdateEntityCommand<TEntity>(int Id) : IRequest<Result<TEntity>>, IRequestWithId
    where TEntity : class, IEntity;