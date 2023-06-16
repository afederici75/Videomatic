namespace Company.Videomatic.Domain.Abstractions;

public interface IEntity
{
    int Id { get; }

    void SetId(int id);
}