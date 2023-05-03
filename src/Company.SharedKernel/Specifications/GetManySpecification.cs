namespace Company.SharedKernel.Specifications;

public class GetManySpecification<TEntity> : Specification<TEntity>
    where TEntity : class
{

    public GetManySpecification(int take = 10, int? skip = null, string[]? includes = default, string[]? orderBy = default)
        : base()
        => Query.DefaultPagedQuery(take, skip, includes, orderBy);

    public GetManySpecification(int[] ids, string[]? includes = default, string[]? orderBy = default, int take = 10, int? skip = null)
        : base()
    {
        Query.DefaultPagedQuery(take, skip, includes, orderBy);
        
        // TODO: possible smell here
        if (typeof(IEntity).IsAssignableFrom(typeof(TEntity)))
            Query.Where(e => ids.Contains(((IEntity)e).Id));
    }
}