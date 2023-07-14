using System.Runtime.CompilerServices;

namespace Company.Videomatic.Domain.Abstractions;

public interface IAggregateRoot
{
    object GetId();
}

public interface IAggregateRoot<TId> : IAggregateRoot
{
    TId Id { get; }

    object IAggregateRoot.GetId() => Id ?? throw new Exception("No Id assigned yet.");
    
}