using System.Runtime.CompilerServices;

namespace Company.Videomatic.Domain.Abstractions;

public interface IAggregateRoot
{
    long GetId();
}

public interface ILongId
{ 
    long Value { get; }
}

public interface IAggregateRoot<TId> : IAggregateRoot
    where TId : ILongId
{
    TId Id { get; }

    long IAggregateRoot.GetId() => Id.Value;
    
}