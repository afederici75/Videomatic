namespace SharedKernel.Abstractions;

public interface ITrackable
{
    public DateTime CreatedOn { get; }
    public DateTime? UpdatedOn { get; }

    //public void SetCreatedOn(DateTime? value = default);
    //public void SetUpdatedOn(DateTime? value = default);
}