using SharedKernel.Abstractions;

namespace SharedKernel.CQRS.Commands;

public record UpsertEntityCommand<TEntity>(int? Id) : IRequest<Result<TEntity>>
    where TEntity : class;