using Xunit.Abstractions;

namespace Company.Videomatic.Infrastructure.Data.Tests.Base;

public class DbContextFixture : IAsyncLifetime    
{
    public DbContextFixture(VideomaticDbContext dbContext, ITestOutputHelperAccessor outputAccessor)
        : base()
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _outputAccessor = outputAccessor ?? throw new ArgumentNullException(nameof(outputAccessor));

        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    readonly ITestOutputHelperAccessor _outputAccessor;
    public ITestOutputHelper Output => _outputAccessor.Output ?? throw new Exception("XXX");


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

        throw new NotImplementedException();


        // Loads all videos from the TestData folder
        //var allVideos = await VideoDataGenerator.CreateAllVideos(true);
        //DbContext.AddRange(allVideos);
        //await DbContext.SaveChangesAsync();
    }
}
