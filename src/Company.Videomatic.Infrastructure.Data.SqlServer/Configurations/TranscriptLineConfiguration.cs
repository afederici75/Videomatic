namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class TranscriptLineConfiguration : TranscriptLineConfigurationBase
{
    public override void Configure(EntityTypeBuilder<TranscriptLine> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}