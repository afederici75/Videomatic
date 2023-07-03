namespace Company.Videomatic.Domain.Extensions;

public static class IReadRepositoryExtensions
{
    public static async Task<PageResult<T>> PageAsync<T>(
        this IReadRepository<T> repository,
        IPaginatedSpecification<T> specification,
        CancellationToken cancellationToken = default)
        where T : class, IAggregateRoot
    {
        var videos = await repository.ListAsync(specification, cancellationToken);
        var totalCount = await repository.CountAsync(specification, cancellationToken);

        return new PageResult<T>(
            videos,
            specification.Page,
            specification.PageSize,
            totalCount);
    }
}
