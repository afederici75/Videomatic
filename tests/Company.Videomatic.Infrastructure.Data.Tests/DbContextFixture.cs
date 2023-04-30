using Company.Videomatic.Infrastructure.Data;

namespace Company.Videomatic.Application.Tests;

public class DbContextFixture<TDBContext> : IAsyncLifetime
    where TDBContext : VideomaticDbContext
{
    public DbContextFixture(TDBContext dbContext)
        : base()
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    protected bool SkipInsertTestData { get; set; }
    [Obsolete("This is a hack to check the database data if tests don't run successfully.")]
    public bool SkipDeletingDatabase { get; set; }

    public TDBContext DbContext { get; }

    public virtual Task DisposeAsync()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        if (!SkipDeletingDatabase)
            DbContext.Database.EnsureDeleted();
#pragma warning restore CS0618 // Type or member is obsolete

        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        if (SkipInsertTestData)
            return;

        // Loads all videos from the TestData folder
        var allVideos = await VideoDataGenerator.CreateAllVideos(true);
        DbContext.AddRange(allVideos);
        await DbContext.SaveChangesAsync();
    }
}
