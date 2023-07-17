using Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;
using Company.Videomatic.Infrastructure.SqlServer.Configurations;
using Microsoft.Extensions.Configuration;

namespace Company.Videomatic.Infrastructure.Data.SqlServer;

public class SqlServerVideomaticDbContext : VideomaticDbContext
{
    public const string ProviderName = "SqlServer";
    public const string SequenceName = "MainId";    

    public SqlServerVideomaticDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.HasSequence<long>(TranscriptConfiguration.SequenceName); // TODO: fix this
        modelBuilder.HasSequence<long>(TranscriptConfiguration.TranscriptLineSequenceName); // TODO: fix this
        modelBuilder.HasSequence<long>(VideoConfiguration.SequenceName); // TODO: fix this
        modelBuilder.HasSequence<long>(VideoConfiguration.ThumbnailSequenceName); // TODO: fix this
        modelBuilder.HasSequence<long>(VideoConfiguration.TagsSequenceName); // TODO: fix this
        modelBuilder.HasSequence<long>(PlaylistConfiguration.SequenceName); // TODO: fix this
        modelBuilder.HasSequence<long>(ArtifactConfiguration.SequenceName); // TODO: fix this
    }    
}
