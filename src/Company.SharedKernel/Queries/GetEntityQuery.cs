namespace Company.SharedKernel.Queries;

public class GetEntityQuery<TEntity> : QueryBase<TEntity>,
    ISingleResultSpecification<TEntity>
    where TEntity : class, IEntity
{
    protected GetEntityQuery(string[]? includes = null)
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
        : this(includes)
    {
        Query.Where(e => e.Id == id);        
    }
}