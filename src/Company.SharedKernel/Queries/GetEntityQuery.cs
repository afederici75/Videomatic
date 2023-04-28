namespace Company.SharedKernel.Queries;

public class GetEntityQuery<TEntity> : QueryBase<TEntity>,
    ISingleResultSpecification<TEntity>
    where TEntity : class, IEntity
{
    protected GetEntityQuery(int take = 10,
        int? skip = null,
        string[]? includes = null)
        : base(take, skip, includes)
    {
        if (includes != null)
        {
            foreach (var include in includes)
            {
                Query.Include(include);
            }
        }
    }
    public GetEntityQuery(int id, string[]? includes = null)
        : base(includes: includes)
    {
        Query.Where(e => e.Id == id);        
    }
}