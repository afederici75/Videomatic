using Ardalis.Specification.EntityFrameworkCore;
using MediatR;
using System.Threading;

namespace Company.Videomatic.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(VideomaticDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    readonly VideomaticDbContext _dbContext;

    //public async Task<int> DeleteByIdAsync(long id, CancellationToken cancellationToken = default)
    //{
    //    long longId = Convert.ToInt64(id);
    //
    //    int cnt = await _dbContext
    //        .Set<T>()   
    //        .Where(x => EF.Property<long>(x, "Id") == longId)
    //        .ExecuteDeleteAsync(cancellationToken);
    //
    //    return cnt;
    //}
}
