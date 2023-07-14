namespace Company.Videomatic.Application.Abstractions;

public interface ICRUDCommand<TAggregateRoot>
    where TAggregateRoot : IAggregateRoot
{
    Type GetAggregateRootType() => typeof(TAggregateRoot);
}

public interface ICreateCommand<TAggregateRoot> : IRequest<Result<long>>
    where TAggregateRoot : class, IAggregateRoot
{ }

public interface IDeleteCommand<TAggregateRoot> : IRequest<Result<long>>
    where TAggregateRoot : class, IAggregateRoot
{
    public long Id { get; }
}

public interface IUpdateCommand<TAggregateRoot> : IRequest<Result<long>>
    where TAggregateRoot : class, IAggregateRoot
{ 
    //public long Id { get; }
}