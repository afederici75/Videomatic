using SharedKernel.Abstractions;

namespace SharedKernel;

public abstract class UpdateableEntity<TId> : Entity<TId>, IUpdateableEntity
{
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedOn { get; private set; }

    public void SetCreatedOn(DateTime? value = default)
    {
        CreatedOn = value ?? DateTime.UtcNow;
        UpdatedOn = null;
    }

    public void SetUpdatedOn(DateTime? value = default)
    { 
        UpdatedOn = value ?? DateTime.UtcNow;
    }
}