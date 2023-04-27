namespace Company.SharedKernel;

// Inspired by https://github.com/dotnet-architecture/eShopOnWeb/blob/9db2feb930c8cd1ce379bbebf76521a4ba0dddfb/src/ApplicationCore/Entities/BaseEntity.cs#L5

/// <summary>
/// The base class for all entities.
/// </summary>
public abstract class EntityBase : IEntity
{
    public virtual int Id { get; protected set; }

    public void SetId(int id)
    {
        Guard.Against.Negative(id, nameof(id));

        Id = id;
    }
}