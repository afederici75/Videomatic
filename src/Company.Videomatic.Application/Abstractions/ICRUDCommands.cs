namespace Company.Videomatic.Application.Abstractions;

public interface IAggregateRootCommand<TAggregateRoot> 
    where TAggregateRoot : class, IAggregateRoot
{ }


public interface ICreateCommand<TAggregateRoot> : IAggregateRootCommand<TAggregateRoot>, IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot
{ }

public interface IDeleteCommand<TAggregateRoot> : IAggregateRootCommand<TAggregateRoot>, IRequest<Result<bool>>
    where TAggregateRoot : class, IAggregateRoot
{
    public long Id { get; }
}

public interface IUpdateCommand<TAggregateRoot> : IAggregateRootCommand<TAggregateRoot>, IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot
{ 
    public long Id { get; }
}