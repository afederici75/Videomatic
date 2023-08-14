namespace Infrastructure.Data.SqlServer.Configurations;

public class ArtifactConfiguration : ArtifactConfigurationBase 
{
    public const string SequenceName = "ArtifactSequence";

    public override void Configure(EntityTypeBuilder<Artifact> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {SequenceName}"); // TODO: unhardcode
    }
}