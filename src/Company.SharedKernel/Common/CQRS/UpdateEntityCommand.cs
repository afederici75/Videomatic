using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.Common.CQRS;

public record UpdateEntityCommand<TEntity>(int Id) : IRequest<Result<TEntity>>
    where TEntity : class, IEntity;