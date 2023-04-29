namespace Company.Videomatic.Infrastructure.SqlServer.Tests;

public class VideomaticDbContextFixture : VideomaticRepositoryFixture, IDisposable, IAsyncLifetime
{
    public VideomaticDbContextFixture(VideomaticDbContext dbContext, IRepositoryBase<Video> repository)
        : base(repository)
    {
        DbContext = dbContext;
        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();        
    }

    public void Dispose()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        if (base.SkipDeletingDatabase)
            return;
#pragma warning restore CS0618 // Type or member is obsolete
        
        DbContext.Database.EnsureDeleted();
    }

    public VideomaticDbContext DbContext { get; }    
}
