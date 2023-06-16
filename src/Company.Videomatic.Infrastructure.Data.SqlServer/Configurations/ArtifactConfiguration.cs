using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class ArtifactConfiguration : Data.Configurations.ArtifactConfigurationBase 
{
    public override void Configure(EntityTypeBuilder<Artifact> builder)
    {
        base.Configure(builder);

        //builder.OverrideIEntityForSqlServer();
    }
}
