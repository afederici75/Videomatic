using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class TranscriptConfiguration : EntityConfigurationBase<Transcript>
{
    public override void Configure(EntityTypeBuilder<Transcript> builder)
    {
        base.Configure(builder);
        // Fields
        
        // Relationships
        builder.HasMany(x => x.Lines)
               .WithOne()
               .IsRequired(true);

        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}   