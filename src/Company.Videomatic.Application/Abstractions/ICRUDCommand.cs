namespace Company.Videomatic.Application.Abstractions;

public interface ICreateCommand<TAggregateRoot> : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot
{ }

public interface IDeleteCommand<TAggregateRoot> : IRequest<Result<bool>>
    where TAggregateRoot : class, IAggregateRoot
{
    public long Id { get; }
}

public interface IUpdateCommand<TAggregateRoot> : IRequest<Result<TAggregateRoot>>
    where TAggregateRoot : class, IAggregateRoot
{ 
    public long Id { get; }
}