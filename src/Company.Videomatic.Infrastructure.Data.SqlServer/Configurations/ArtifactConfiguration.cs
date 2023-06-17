
namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class ArtifactConfiguration : ArtifactDbConfigurationBase 
{
    public override void Configure(EntityTypeBuilder<ArtifactDb> builder)
    {
        base.Configure(builder);

        //builder.OverrideIEntityForSqlServer();
    }
}
