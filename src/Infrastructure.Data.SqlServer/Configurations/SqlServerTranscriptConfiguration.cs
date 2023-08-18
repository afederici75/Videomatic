using Infrastructure.Data.Configurations.Entities;

namespace Infrastructure.SqlServer.Configurations;

public class SqlServerTranscriptConfiguration : TranscriptConfiguration
{
    public const string TableName = "Transcripts";

    public const string SequenceName = "TranscriptSequence";

    public override void Configure(EntityTypeBuilder<Transcript> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName, Constants.VideomaticDbSchema);

        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {SequenceName}")
               .IsRequired();
    }
}   