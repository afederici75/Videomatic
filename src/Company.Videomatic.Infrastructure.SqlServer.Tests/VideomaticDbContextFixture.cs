namespace Company.Videomatic.Infrastructure.SqlServer.Tests;

public class VideomaticDbContextFixture : IDisposable
{
    public VideomaticDbContextFixture(VideomaticDbContext context)
    {
        DbContext = context;
        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    bool _skipDeletingDatabase = false;
    [Obsolete("This is a hack to check the database data if tests don't run successfully.")]
    public void SkipDeletingDatabase()
    { 
        _skipDeletingDatabase = true;
    }

    public VideomaticDbContext DbContext { get; }

    public void Dispose()
    {
        if (!_skipDeletingDatabase)
            DbContext.Database.EnsureDeleted();
    }
}
