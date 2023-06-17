using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class VideoDbConfiguration : VideoDbConfigurationBase
{
    public override void Configure(EntityTypeBuilder<VideoDb> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}