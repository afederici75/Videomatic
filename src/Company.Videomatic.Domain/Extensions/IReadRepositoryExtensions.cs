using Ardalis.Specification;

namespace Company.Videomatic.Domain.Extensions;

public static class IReadRepositoryExtensions
{
    public static async Task<PageResult<TDEST>> PageAsync<TSRC, TDEST>(
        this IReadRepository<TSRC> repository,
        ISpecification<TSRC> specification,
        int page,
        int pageSize,
        Func<TSRC, TDEST> map,
        CancellationToken cancellationToken = default)
        where TSRC : class, IAggregateRoot
        where TDEST : class
    {
        var aggrs = await repository.ListAsync(specification, cancellationToken);
        var totalCount = await repository.CountAsync(specification, cancellationToken);

        return new PageResult<TDEST>(aggrs.Select(map), page, pageSize, totalCount);        
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