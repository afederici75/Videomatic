namespace Company.Videomatic.Infrastructure.SqlServer.Tests;

public class VideomaticDbContextFixture : IDisposable
{
    public VideomaticDbContextFixture(VideomaticDbContext context)
    {
        DbContext = context;
        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    public VideomaticDbContext DbContext { get; }

    public void Dispose()
    {
        //DbContext.Database.EnsureDeleted();
    }
}
