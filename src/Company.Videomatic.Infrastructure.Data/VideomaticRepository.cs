using Company.Videomatic.Domain.Abstractions;

namespace Company.Videomatic.Infrastructure.Data;

public abstract class RepositoryBase<TDBCONTEXT, TAGGREGATEROOT> : IRepository<TAGGREGATEROOT>//, IReadOnlyRepository<TAGGREGATEROOT>
    where TDBCONTEXT : DbContext
    where TAGGREGATEROOT : class, IAggregateRoot
{
    public RepositoryBase(TDBCONTEXT dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    protected TDBCONTEXT DbContext { get; }

    //public abstract Task<IEnumerable<TAGGREGATEROOT>> AddRangeAsync(IEnumerable<TAGGREGATEROOT> entities, CancellationToken cancellationToken = default);
    //public abstract Task DeleteRangeAsync(IEnumerable<TAGGREGATEROOT> entities, CancellationToken cancellationToken = default);
    //public abstract Task<TAGGREGATEROOT?> GetByIdAsync(int id, IEnumerable<string>? includes = null, CancellationToken cancellationToken = default);
    //public abstract Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    //public abstract Task UpdateRangeAsync(IEnumerable<TAGGREGATEROOT> entities, CancellationToken cancellationToken = default);
}

public class VideomaticRepository<T> : RepositoryBase<VideomaticDbContext, T>
    where T : class, IAggregateRoot
{
    private readonly VideomaticDbContext _dbContext;

    public VideomaticRepository(VideomaticDbContext dbContext) 
    {
        _dbContext = dbContext;             
    }

    //public async Task<T?> GetByIdAsync(int id, IEnumerable<string>? includes = null, CancellationToken cancellationToken = default)
    //{
    //    throw new NotImplementedException();
    //    //var qry = new GetOneSpecification<T>(id, includes?.ToArray());
    //    //var res = await  base.FirstOrDefaultAsync(qry, cancellationToken);            
    //    //return res;
    //}

    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    throw new NotImplementedException();
    //    //var res = await base.SaveChangesAsync(cancellationToken);
    //    //_dbContext.ChangeTracker.Clear(); // IMPORTANT!
    //    //return res;
    //}    
}
