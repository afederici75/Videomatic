namespace Company.Videomatic.Domain.Abstractions;

public interface IEntity<TKEY>    
{
    TKEY Id { get; }

    void SetId(TKEY id);
}