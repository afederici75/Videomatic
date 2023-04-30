using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class ThumbnailConfiguration : ThumbnailConfigurationBase
{
    public override void Configure(EntityTypeBuilder<Thumbnail> builder)
    {
        base.Configure(builder);

        builder.OverrideIEntityForSqlServer();
    }
}