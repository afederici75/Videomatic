namespace Company.SharedKernel.Abstractions;

public interface IUpdateCommand<TAggregateRoot> : IAggregateRootCommand<TAggregateRoot>, IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot
{ 
    public long Id { get; }
}