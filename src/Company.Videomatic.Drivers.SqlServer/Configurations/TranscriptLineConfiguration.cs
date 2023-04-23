using Company.Videomatic.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Drivers.SqlServer.Configurations;

public class TranscriptLineConfiguration : IEntityTypeConfiguration<TranscriptLine>
{
    public void Configure(EntityTypeBuilder<TranscriptLine> builder)
    {
        // Fields
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {DbConstants.SequenceName}");
        
        //Indices
        //builder.HasIndex(x => x.Id).IsUnique();
        builder.HasIndex(x => x.Text);
    }
}
