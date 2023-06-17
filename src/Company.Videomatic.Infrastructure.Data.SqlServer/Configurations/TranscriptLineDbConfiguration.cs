using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class TranscriptLineDbConfiguration : TranscriptLineDbConfigurationBase
{
    public override void Configure(EntityTypeBuilder<TranscriptLineDb> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}