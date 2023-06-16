using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class TranscriptLineConfiguration : TranscriptConfigurationBase
{
    public override void Configure(EntityTypeBuilder<Transcript> builder)
    {
        base.Configure(builder);

        //builder.OverrideIEntityForSqlServer();
    }
}