namespace Company.Videomatic.Infrastructure.Data.Model;

public abstract class EntityBase : IEntity
{
    public long Id { get; private set; }
}

