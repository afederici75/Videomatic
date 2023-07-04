namespace Company.Videomatic.Domain.Extensions;

public static class IReadRepositoryExtensions
{
    public static async Task<PageResult<TDEST>> PageAsync<TSRC, TDEST>(
        this IReadRepository<TSRC> repository,
        ISpecification<TSRC> specification,
        Func<TSRC, TDEST> map,
        int? page = null,
        int? pageSize = null,
        CancellationToken cancellationToken = default)
        where TSRC : class, IAggregateRoot
        where TDEST : class
    {
        var aggRoots = await repository.ListAsync(specification, cancellationToken);
        var totalCount = await repository.CountAsync(specification, cancellationToken);

        return new PageResult<TDEST>(aggRoots.Select(map), page ?? 1, pageSize ?? 10, totalCount);        
    }

    public record ListAndCountResult<T>(List<T> List, int TotalCount) where T : class;

    public static async Task<ListAndCountResult<T>> ListAndCountAsync<T>(
        this IReadRepository<T> repository,
        ISpecification<T> specification,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
        where T : class, IAggregateRoot
    {
        var videos = await repository.ListAsync(specification, cancellationToken);
        var totalCount = await repository.CountAsync(specification, cancellationToken);

        return new ListAndCountResult<T>(videos, totalCount);
    }
}