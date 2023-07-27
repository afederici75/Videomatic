using Microsoft.Extensions.Configuration;

namespace Infrastructure.Tests.Data.Helpers;

public class DbContextFixture : IAsyncLifetime
{
    public DbContextFixture(
        IDbContextFactory<VideomaticDbContext> dbContextFactory,
        ITestOutputHelperAccessor outputAccessor,
        IDbSeeder seeder,
        IConfiguration configuration)
        : base()
    {
        DbContext = dbContextFactory?.CreateDbContext() ?? throw new ArgumentNullException(nameof(dbContextFactory));

        _outputAccessor = outputAccessor ?? throw new ArgumentNullException(nameof(outputAccessor));
        Seeder = seeder ?? throw new ArgumentNullException(nameof(seeder));
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        //try
        //{
            DbContext.Database.EnsureDeleted();
        //}
        //catch
        //{ }
        DbContext.Database.Migrate();
    }    

    readonly ITestOutputHelperAccessor _outputAccessor;
    readonly IDbSeeder Seeder;

    public ITestOutputHelper Output => _outputAccessor.Output!;


    protected bool SkipInsertTestData { get; set; } = false;
    [Obsolete("This is a hack to check the database data if tests don't run successfully.")]
    public bool SkipDeletingDatabase { get; set; }

    public VideomaticDbContext DbContext { get; }    
    public IConfiguration Configuration { get; }

    public virtual Task DisposeAsync()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        if (!SkipDeletingDatabase)
            return DbContext.Database.EnsureDeletedAsync();
#pragma warning restore CS0618 // Type or member is obsolete

        
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        if (SkipInsertTestData)
            return;

        await Seeder.SeedAsync();
    }
}
