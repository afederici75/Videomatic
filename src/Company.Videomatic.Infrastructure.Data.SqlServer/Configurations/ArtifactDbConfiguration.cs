
namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class ArtifactDbConfiguration : ArtifactDbConfigurationBase 
{
    public override void Configure(EntityTypeBuilder<ArtifactDb> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}
