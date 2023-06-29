namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class TranscriptConfiguration : TranscriptConfigurationBase
{
    public const string SequenceName = "TranscriptSequence";
    public override void Configure(EntityTypeBuilder<Transcript> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {SequenceName}"); // TODO: unhardcode

        builder.HasIndex(x => x.Id)
             .IsUnique();

    }
}   