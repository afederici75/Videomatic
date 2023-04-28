namespace Ardalis.Specification;

public static class ArdalisSpecificationBuilderExtensions
{
    /// <summary>
    /// Sets AsNoTracking, OrderBy(x => x.Id), and inclusions.
    /// </summary>    
    public static ISpecificationBuilder<TEntity> DefaultQuery<TEntity>(this ISpecificationBuilder<TEntity> @this, 
        string[]? includes = default,
        string[]? orderBy = default)
        where TEntity : class
    {
        @this.AsNoTracking()
             .Include(includes)
             .OrderBy(orderBy);

        return @this;
    }
    
    /// <summary>
    /// Sets AsNoTracking, OrderBy(x => x.Id), inclusions, and take/skip.
    /// </summary>
    public static ISpecificationBuilder<TEntity> DefaultPagedQuery<TEntity>(
        this ISpecificationBuilder<TEntity> @this,
        int take = 10,
        int? skip = null, 
        string[]? includes = default,
        string[]? orderBy = default)
        where TEntity : class
    {
        @this.DefaultQuery(includes, orderBy)
             .Skip(skip ?? 0)
             .Take(take);

        return @this;
    }

    /// <summary>
    /// Adds the specified includes to the query.
    /// </summary>
    public static ISpecificationBuilder<TEntity> Include<TEntity>(this ISpecificationBuilder<TEntity> @this,
        string[]? includes)
        where TEntity : class
    {
        if (includes is null)
            return @this;

        foreach (var item in includes)
        {
            @this.Include(item);
        }

        return @this;
    }
}