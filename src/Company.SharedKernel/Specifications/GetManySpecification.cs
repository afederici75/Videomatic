namespace Company.SharedKernel.Specifications;

public class GetManySpecification<TEntity> : Specification<TEntity>
    where TEntity : class, IEntity
{

    public GetManySpecification(int take = 10, int? skip = null, string[]? includes = default, string[]? orderBy = default)
        : base()
        => Query.DefaultPagedQuery(take, skip, includes);

    public GetManySpecification(int[] ids, string[]? includes = default, string[]? orderBy = default)
        : base()
        =>  Query.DefaultQuery(includes, orderBy)
                 .Where(e => ids.Contains(e.Id));
}