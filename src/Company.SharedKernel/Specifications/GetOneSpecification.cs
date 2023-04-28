namespace Company.SharedKernel.Specifications;

public class GetOneSpecification<TEntity> : Specification<TEntity>,
    ISingleResultSpecification<TEntity>
    where TEntity : class, IEntity
{
    protected GetOneSpecification(string[]? includes = null)
        => Query.DefaultQuery(includes);

    public GetOneSpecification(int id, string[]? includes = null) 
        => Query.DefaultQuery(includes)
                .Where(e => e.Id == id);       
    
}