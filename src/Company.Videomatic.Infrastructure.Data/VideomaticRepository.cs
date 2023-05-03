using Company.SharedKernel.Specifications;

namespace Company.Videomatic.Infrastructure.Data;

public class VideomaticRepository<T> : RepositoryBase<T>, IRepository<T>, IReadOnlyRepository<T>
    where T : class, IEntity
{
    private readonly VideomaticDbContext _dbContext;

    public VideomaticRepository(VideomaticDbContext dbContext) 
        : base(dbContext)
    {
        _dbContext = dbContext;             
    }

    public async Task<T?> GetByIdAsync(int id, IEnumerable<string>? includes = null, CancellationToken cancellationToken = default)
    {        
        var qry = new GetOneSpecification<T>(id, includes?.ToArray());
        var res = await  base.FirstOrDefaultAsync(qry, cancellationToken);            
        return res;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var res = await base.SaveChangesAsync(cancellationToken);
        _dbContext.ChangeTracker.Clear(); // IMPORTANT!
        return res;
    }    
}
