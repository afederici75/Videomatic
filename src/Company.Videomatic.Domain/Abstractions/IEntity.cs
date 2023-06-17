namespace Company.Videomatic.Domain.Abstractions;

/// <summary>
/// A marker interface for entities.
/// </summary>
public interface IEntity
{ 
    long Id { get; }
}