namespace SharedKernel.Model;

// TODO: Refactor using type like UserAction(UserId, DateTime)? Instead of CBy/COn and UBy/UOn we'd have Created.By/On and Updated.By/On

/// <summary>
/// The base class for entities that are tracked. 
/// </summary>
public abstract class TrackedEntity
{
    public DateTime CreatedOn { get; private set; }    
    public string CreatedBy { get; private set; } = default!;
    public DateTime? UpdatedOn { get; private set; }
    public string? UpdatedBy { get; private set; }
}