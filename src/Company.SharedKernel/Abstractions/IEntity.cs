namespace Company.SharedKernel.Abstractions;

public interface IEntity
{
    int Id { get; }

    void SetId(int id);
}
