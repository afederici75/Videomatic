namespace Company.Videomatic.Domain;

public abstract class Entity<TId> : IEntity
{
    public TId Id { get; protected set; } = default!;
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedOn { get; private set; }

    public void SetUpdatedOn()
    { 
        UpdatedOn = DateTime.UtcNow;
    }

    int IEntity.Id => Convert.ToInt32(Id);
}
