using Company.Videomatic.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class ThumbnailConfiguration : IEntityTypeConfiguration<Thumbnail>
{
    public void Configure(EntityTypeBuilder<Thumbnail> builder)
    {
        // Fields
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {DbConstants.SequenceName}");

        builder.Property(x => x.Url)
               .HasMaxLength(DbConstants.FieldLengths.Url);               

        // Indices
        builder.HasIndex(x => x.Resolution);
        builder.HasIndex(x => x.Url);
        builder.HasIndex(x => x.Height);
        builder.HasIndex(x => x.Width);
    }
}
