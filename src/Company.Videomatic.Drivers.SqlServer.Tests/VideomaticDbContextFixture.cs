namespace Company.Videomatic.Drivers.SqlServer.Tests;

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
        //Context.Database.EnsureDeleted();
    }
}
