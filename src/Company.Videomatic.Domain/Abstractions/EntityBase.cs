namespace Company.Videomatic.Domain.Abstractions;

public abstract class EntityBase : IEntity
{
    public long Id { get; private set; }
}

