namespace Company.Videomatic.Infrastructure.SqlServer;

public class VideomaticRepository<T> : RepositoryBase<T>, IRepositoryBase<T>, IReadRepositoryBase<T>
    where T : class
{
    private readonly VideomaticDbContext _dbContext;

    public VideomaticRepository(VideomaticDbContext dbContext) 
        : base(dbContext)
    {
        _dbContext = dbContext;             
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var res = await base.SaveChangesAsync(cancellationToken);
        _dbContext.ChangeTracker.Clear(); // IMPORTANT!
        return res;
    }    
}
