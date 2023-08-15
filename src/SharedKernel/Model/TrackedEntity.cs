namespace SharedKernel.Model;

public abstract class TrackedEntity
{
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }
    
    public string CreatedBy { get; private set; } = default!;
    public string? UpdatedBy { get; private set; }
}