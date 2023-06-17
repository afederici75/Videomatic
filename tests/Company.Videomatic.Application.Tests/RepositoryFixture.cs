namespace Company.Videomatic.Application.Tests;

public class RepositoryFixture<T> : IAsyncLifetime
    where T : class, IAggregateRoot
{
    public RepositoryFixture(VideomaticDbContext dbContext, IRepository<T> repository, ITestOutputHelperAccessor outputAccessor)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _outputAccessor = outputAccessor ?? throw new ArgumentNullException(nameof(outputAccessor));

        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    public VideomaticDbContext DbContext { get; }
    public IRepository<T> Repository { get; }

    readonly ITestOutputHelperAccessor _outputAccessor;

    public ITestOutputHelper Output => _outputAccessor.Output ?? throw new Exception("XXX");

    protected bool SkipInsertTestData { get; set; }
    [Obsolete("This is a hack to check the database data if tests don't run successfully.")]
    public bool SkipDeletingDatabase { get; set; }

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

