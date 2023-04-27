namespace Company.SharedKernel;

// Inspired by https://github.com/dotnet-architecture/eShopOnWeb/blob/9db2feb930c8cd1ce379bbebf76521a4ba0dddfb/src/ApplicationCore/Entities/BaseEntity.cs#L5

// This can easily be modified to be BaseEntity<T> and public T Id to support different key types.
// Using non-generic integer types for simplicity and to ease caching logic
public abstract class BaseEntity : IEntity
{
    public virtual int Id { get; protected set; }

    public void SetId(int id)
    {
        Guard.Against.Negative(id, nameof(id));

        Id = id;
    }
}