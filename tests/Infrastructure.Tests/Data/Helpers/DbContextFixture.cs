using Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;
using Company.Videomatic.Infrastructure.SqlServer.Configurations;
using Microsoft.EntityFrameworkCore;
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

        DbContext.Database.ExecuteSqlRaw("delete from dbo.Videos");
        DbContext.Database.ExecuteSqlRaw("delete from dbo.Playlists");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {ArtifactConfiguration.SequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {PlaylistConfiguration.SequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {VideoConfiguration.SequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {TranscriptConfiguration.SequenceName} RESTART WITH 1");


        DbContext.Database.ExecuteSqlRaw($"alter sequence {TranscriptConfiguration.SequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {TranscriptConfiguration.TranscriptLineSequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {VideoConfiguration.SequenceName} RESTART WITH 1");
        //DbContext.Database.ExecuteSqlRaw($"alter sequence {VideoConfiguration.ThumbnailSequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {VideoConfiguration.TagsSequenceName} RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {PlaylistConfiguration.SequenceName}  RESTART WITH 1");
        DbContext.Database.ExecuteSqlRaw($"alter sequence {ArtifactConfiguration.SequenceName} RESTART WITH 1");


        await _seeder.SeedAsync();

        // TODO: Refactor
        DbContext.Database.ExecuteSqlRaw("alter fulltext index on dbo.Videos start full population");
        DbContext.Database.ExecuteSqlRaw("alter fulltext index on dbo.Playlists start full population");
        DbContext.Database.ExecuteSqlRaw("alter fulltext index on dbo.Transcripts start full population");
        DbContext.Database.ExecuteSqlRaw("alter fulltext index on dbo.Artifacts start full population");

        // TODO: I should do what's described here: https://stackoverflow.com/questions/2727911/how-can-i-know-when-sql-full-text-index-population-is-finished
        // For now this seems to work on my machine.
        await Task.Delay(1500);
    }

    //static bool _dbMigrated = false;
    static bool _dbSeeded = false;
    readonly ITestOutputHelperAccessor _outputAccessor;
    readonly IDbSeeder _seeder;
}
