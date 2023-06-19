
namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class ArtifactDbConfiguration : ArtifactConfigurationBase 
{
    public override void Configure(EntityTypeBuilder<Artifact> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}
