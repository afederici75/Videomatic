using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class TranscriptDbConfiguration : TranscriptDbConfigurationBase
{
    public override void Configure(EntityTypeBuilder<TranscriptDb> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}   