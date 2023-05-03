namespace Ardalis.Specification;

public static class SpecificationExtensions
{
    public static Task<List<T>> ToListAsync<T>(this IQueryable<T> query, ISpecification<T> specification, CancellationToken cancellationToken = default)
        where T : class
        => query.WithSpecification(specification)
                .ToListAsync(cancellationToken);
    
    public static Task<T> SingleAsync<T>(this IQueryable<T> query, ISpecification<T> specification, CancellationToken cancellationToken = default)
        where T : class
        => query.WithSpecification(specification)
                .SingleAsync(cancellationToken);
    
    public static Task<T[]> ToArrayAsync<T>(this IQueryable<T> query, ISpecification<T> specification, CancellationToken cancellationToken = default)
        where T : class
        => query.WithSpecification(specification)
                .ToArrayAsync(cancellationToken);
}
