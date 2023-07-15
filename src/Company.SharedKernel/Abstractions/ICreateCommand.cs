namespace Company.SharedKernel.Abstractions;

public interface ICreateCommand<TAggregateRoot> : IAggregateRootCommand<TAggregateRoot>, IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot
{ }
