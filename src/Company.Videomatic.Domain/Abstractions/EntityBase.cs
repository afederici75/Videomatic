namespace Company.Videomatic.Domain.Abstractions;

// Inspired by https://github.com/dotnet-architecture/eShopOnWeb/blob/9db2feb930c8cd1ce379bbebf76521a4ba0dddfb/src/ApplicationCore/Entities/BaseEntity.cs#L5

/// <summary>
/// The base class for all entities.
/// </summary>
public abstract class EntityBase<TKEY> : IEntity<TKEY>
{
    public virtual TKEY Id { get; private set; }

    public void SetId(TKEY id)
    {
        Id = id;
    }
}