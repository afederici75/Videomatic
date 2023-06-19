namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class TranscriptLineDbConfiguration : TranscriptLineConfigurationBase
{
    public override void Configure(EntityTypeBuilder<TranscriptLine> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}