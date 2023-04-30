using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class FolderConfiguration : FolderConfigurationBase 
{
    public override void Configure(EntityTypeBuilder<Folder> builder)
    {
        base.Configure(builder);

        builder.OverrideIEntityForSqlServer();
    }
}
