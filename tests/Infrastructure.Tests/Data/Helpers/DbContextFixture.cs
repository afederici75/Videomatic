using Infrastructure.Data.SqlServer;
using Infrastructure.Data.SqlServer.Configurations;
using Infrastructure.SqlServer.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Tests.Data.Helpers;

public class DbContextFixture : IAsyncLifetime
{
    public DbContextFixture(
        IDbContextFactory<SqlServerVideomaticDbContext> dbContextFactory,
        ITestOutputHelperAccessor outputAccessor,
        IDbSeeder seeder,
        IConfiguration configuration)
        : base()
    {
        DbContext = dbContextFactory?.CreateDbContext() ?? throw new ArgumentNullException(nameof(dbContextFactory));

        _outputAccessor = outputAccessor ?? throw new ArgumentNullException(nameof(outputAccessor));
        _seeder = seeder ?? throw new ArgumentNullException(nameof(seeder));
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));        
    }    

    public ITestOutputHelper Output => _outputAccessor.Output!;

    //[Obsolete("This is a hack to check the database data if tests don't run successfully.")]
    //public bool SkipDeletingDatabase { get; set; }

    public VideomaticDbContext DbContext { get; }    
    public IConfiguration Configuration { get; }

    public virtual Task DisposeAsync()
    {
//#pragma warning disable CS0618 // Type or member is obsolete
//        //if (!SkipDeletingDatabase)
//        //    return DbContext.Database.EnsureDeletedAsync();
//#pragma warning restore CS0618 // Type or member is obsolete

        DbContext.Dispose();

        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        if (_dbSeeded)
            return;
        
        _dbSeeded = true;

        DbContext.Database.ExecuteSqlRaw($"delete from {Constants.VideomaticDbSchema}.Videos");
        DbContext.Database.ExecuteSqlRaw($"delete from {Constants.VideomaticDbSchema}.Playlists");

        DbContext.Database.ExecuteSqlRaw($"alter sequence {SqlServerArtifactConfiguration.SequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {SqlServerPlaylistConfiguration.SequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {SqlServerVideoConfiguration.SequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {SqlServerTranscriptConfiguration.SequenceName} RESTART WITH 1");


        DbContext.Database.ExecuteSqlRaw($"alter sequence {SqlServerTranscriptConfiguration.SequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {SqlServerVideoConfiguration.SequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {SqlServerVideoConfiguration.TagsSequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {SqlServerPlaylistConfiguration.SequenceName}  RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {SqlServerArtifactConfiguration.SequenceName} RESTART WITH 1");


        await _seeder.SeedAsync();

        // TODO: Refactor
        DbContext.Database.ExecuteSqlRaw($"alter fulltext index on {Constants.VideomaticDbSchema}.Videos start full population");
        DbContext.Database.ExecuteSqlRaw($"alter fulltext index on {Constants.VideomaticDbSchema}.Playlists start full population");
        DbContext.Database.ExecuteSqlRaw($"alter fulltext index on {Constants.VideomaticDbSchema}.Transcripts start full population");
        DbContext.Database.ExecuteSqlRaw($"alter fulltext index on {Constants.VideomaticDbSchema}.Artifacts start full population");


        // TODO: I should do what's described here: https://stackoverflow.com/questions/2727911/how-can-i-know-when-sql-full-text-index-population-is-finished
        // For now this seems to work on my machine but it makes the tests unreliable after a
        // full database rebuild.
        await Task.Delay(2000);
    }

    //static bool _dbMigrated = false;
    static bool _dbSeeded = false;
    readonly ITestOutputHelperAccessor _outputAccessor;
    readonly IDbSeeder _seeder;
}
