using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class TagConfiguration : Data.Configurations.TagConfigurationBase
{
    public override void Configure(EntityTypeBuilder<Tag> builder)
    {
        base.Configure(builder);
        
        //builder.OverrideIEntityForSqlServer();
    }
}   
