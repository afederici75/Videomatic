using Infrastructure.Data.SqlServer.Configurations;
using Infrastructure.SqlServer.Configurations;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.SqlServer;

public class SqlServerVideomaticDbContext : VideomaticDbContext
{
    public const string ProviderName = "SqlServer";
    public const string SequenceName = "MainId";

    public SqlServerVideomaticDbContext()
        : base()
    {
            
    }

    public SqlServerVideomaticDbContext(DbContextOptions options) : base(options)
    {

    }
  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.HasSequence<long>(SqlServerTranscriptConfiguration.SequenceName); // TODO: fix this
        modelBuilder.HasSequence<long>(SqlServerTranscriptConfiguration.TranscriptLineSequenceName); // TODO: fix this
        modelBuilder.HasSequence<long>(SqlServerVideoConfiguration.SequenceName); // TODO: fix this
        //modelBuilder.HasSequence<long>(VideoConfiguration.ThumbnailSequenceName); // TODO: fix this
        modelBuilder.HasSequence<long>(SqlServerVideoConfiguration.TagsSequenceName); // TODO: fix this
        modelBuilder.HasSequence<long>(SqlServerPlaylistConfiguration.SequenceName); // TODO: fix this
        modelBuilder.HasSequence<long>(SqlServerArtifactConfiguration.SequenceName); // TODO: fix this
    }    
}
