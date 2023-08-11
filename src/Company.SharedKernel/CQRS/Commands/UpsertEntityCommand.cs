using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel.CQRS.Commands;

public record UpsertEntityCommand<TEntity>(int? Id) : IRequest<Result<TEntity>>
    where TEntity : class, IEntity;
