using Company.Videomatic.Infrastructure.SqlServer;

namespace Company.Videomatic.Application.Tests;

public class VideomaticDbContextFixture : IAsyncLifetime
{
    public VideomaticDbContextFixture(VideomaticDbContext dbContext)
        : base()
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        
        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    protected bool SkipInsertTestData { get; set; }
    [Obsolete("This is a hack to check the database data if tests don't run successfully.")]
    public bool SkipDeletingDatabase { get; set; }

    public VideomaticDbContext DbContext { get; }

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
        await DbContext.AddRangeAsync(allVideos);
    }
}
