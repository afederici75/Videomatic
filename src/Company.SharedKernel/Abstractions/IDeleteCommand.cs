namespace Company.SharedKernel.Abstractions;

public interface IDeleteCommand<TAggregateRoot> : IAggregateRootCommand<TAggregateRoot>, IRequest<Result<bool>>
    where TAggregateRoot : class, IAggregateRoot
{
    public long Id { get; }
}
