namespace Domain;

public abstract class UpdateableEntity<TId> : Entity<TId>
{
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedOn { get; private set; }

    public void SetUpdatedOn()
    { 
        UpdatedOn = DateTime.UtcNow;
    }
}