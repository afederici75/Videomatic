using Company.Videomatic.Infrastructure.Data.Handlers;

namespace Company.Videomatic.Infrastructure.Data.Tests.SqlServer;

public class SqlServerDbContextFixture : IAsyncLifetime
{
    public SqlServerDbContextFixture(
        VideomaticDbContext dbContext,
        PlaylistCommandsHandler playlistCommands,
        PlaylistQueriesHandler playListQueries,
        VideoCommandsHandler videoCommands,
        VideoQueriesHandler videoQueries,
        ITestOutputHelperAccessor outputAccessor)
        : base()
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        
        PlaylistCommands = playlistCommands ?? throw new ArgumentNullException(nameof(playlistCommands));
        PlaylistsQueries = playListQueries ?? throw new ArgumentNullException(nameof(playListQueries));
        VideoCommands = videoCommands ?? throw new ArgumentNullException(nameof(videoCommands));
        VideoQueries = videoQueries ?? throw new ArgumentNullException(nameof(videoQueries));
        _outputAccessor = outputAccessor ?? throw new ArgumentNullException(nameof(outputAccessor));

        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    readonly ITestOutputHelperAccessor _outputAccessor;
    public ITestOutputHelper Output => _outputAccessor.Output!;


    protected bool SkipInsertTestData { get; set; }
    [Obsolete("This is a hack to check the database data if tests don't run successfully.")]
    public bool SkipDeletingDatabase { get; set; }

    public VideomaticDbContext DbContext { get; }
    public PlaylistCommandsHandler PlaylistCommands { get; }
    public PlaylistQueriesHandler PlaylistsQueries { get; }
    public VideoCommandsHandler VideoCommands { get; }
    public VideoQueriesHandler VideoQueries { get; }

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

        //throw new NotImplementedException();


        // Loads all videos from the TestData folder
        //var allVideos = await VideoDataGenerator.CreateAllVideos(true);
        //DbContext.AddRange(allVideos);
        //await DbContext.SaveChangesAsync();
    }
}
