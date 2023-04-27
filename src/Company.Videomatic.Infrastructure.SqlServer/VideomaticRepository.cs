namespace Company.Videomatic.Infrastructure.SqlServer;

public class VideomaticRepository<T> : RepositoryBase<T>, IRepository<T>
    where T : class, IAggregateRoot, IEntity
{
    private readonly VideomaticDbContext _dbContext;

    public VideomaticRepository(VideomaticDbContext dbContext) 
        : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
