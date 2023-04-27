namespace Company.SharedKernel.Queries;

public class GetEntitiesQuery<TEntity> : QueryBase<TEntity>
    where TEntity : class, IEntity
{
    public GetEntitiesQuery(int take = 10, int? skip = null, string[]? includes = null) 
        : base(take, skip, includes)
    {
        // Base does everything we need.
    }

    public GetEntitiesQuery(params int[] ids)
        : base()
    {
        Query.Where(e => ids.Contains(e.Id));
    }
}