namespace Company.SharedKernel.Queries;

public abstract class QueryBase<TEntity> : Specification<TEntity>, 
    ISpecification<TEntity>
    where TEntity : class, IEntity
{
    protected QueryBase(
        int take = 10, 
        int? skip = null, 
        string[]? includes = null)
    {
        Query.AsNoTracking();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                Query.Include(include);
            }
        }

        Query.Skip(skip ?? 0)
             .Take(take)
             .OrderBy(x => x.Id);
    }
}
