namespace Company.SharedKernel.Abstractions;

/// <summary>
/// Marker interface for aggregate roots.
/// </summary>
public interface IAggregateRootx : IEntity
{
}

public interface IEntity
{ 
    public int Id { get; }
}