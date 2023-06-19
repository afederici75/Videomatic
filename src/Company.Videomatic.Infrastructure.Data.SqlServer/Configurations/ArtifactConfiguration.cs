
namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class ArtifactConfiguration : ArtifactConfigurationBase 
{
    public override void Configure(EntityTypeBuilder<Artifact> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}
