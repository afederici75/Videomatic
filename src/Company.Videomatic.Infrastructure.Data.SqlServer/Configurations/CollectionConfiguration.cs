using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class CollectionConfiguration : Data.Configurations.CollectionConfigurationBase
{
    public override void Configure(EntityTypeBuilder<Collection> builder)
    {
        base.Configure(builder);
        
        builder.OverrideIEntityForSqlServer();
    }
}