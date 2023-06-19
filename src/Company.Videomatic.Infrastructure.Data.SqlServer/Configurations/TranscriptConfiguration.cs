namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class TranscriptConfiguration : TranscriptConfigurationBase
{
    public override void Configure(EntityTypeBuilder<Transcript> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}   