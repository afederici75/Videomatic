namespace SharedKernel.Abstractions;

public interface IUpdateableEntity
{
    public DateTime CreatedOn { get; }
    public DateTime? UpdatedOn { get; }

    public void SetCreatedOn(DateTime? value = default);
    public void SetUpdatedOn(DateTime? value = default);
}