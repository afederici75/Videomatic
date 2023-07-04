namespace Company.Videomatic.Domain.Abstractions;

/// <summary>
/// A marker interface for aggregate roots.
/// </summary>
public interface IAggregateRoot
{
}

public interface IAggregateRoot<TId> : IAggregateRoot
{
    TId Id { get; }
}