using Company.Videomatic.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Drivers.SqlServer.Configurations;

public class ArtifactConfiguration : IEntityTypeConfiguration<Artifact>
{
    public void Configure(EntityTypeBuilder<Artifact> builder)
    {
        // Fields
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {DbConstants.SequenceName}");

        builder.Property(x => x.Title)
               .HasMaxLength(DbConstants.FieldLengths.ArtifactTitle);

        builder.Property(x => x.Text);
               //.has HasMaxLength(DbConstants.FieldLengths.ArtifactTitle);

        // Indices
        builder.HasIndex(x => x.Title);        
    }
}
