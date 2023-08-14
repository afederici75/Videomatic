using SharedKernel.Abstractions;

namespace SharedKernel;

public abstract class Entity<TId> : IEntity
{
    public TId Id { get; protected set; } = default!;    

    int IEntity.Id => Convert.ToInt32(Id);

    #region Private

    protected Entity()
    {
            
    }

    #endregion
}
