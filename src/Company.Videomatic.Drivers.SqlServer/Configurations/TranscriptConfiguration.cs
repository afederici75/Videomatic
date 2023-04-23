using Company.Videomatic.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Drivers.SqlServer.Configurations;

public class TranscriptConfiguration : IEntityTypeConfiguration<Transcript>
{
    public void Configure(EntityTypeBuilder<Transcript> builder)
    {
        // Fields
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {DbConstants.SequenceName}");
        
        // Relationships
        //builder.HasMany(x => x.Lines)
        //       .WithOne(x => x.Transcript)               
        //       .IsRequired(true);

        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}   