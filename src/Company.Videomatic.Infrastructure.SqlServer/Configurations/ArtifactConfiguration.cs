using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class ArtifactConfiguration : EntityConfigurationBase<Artifact>
{
    public override void Configure(EntityTypeBuilder<Artifact> builder)
    {
        base.Configure(builder);
        // Fields
        
        builder.Property(x => x.Title)
               .HasMaxLength(DbConstants.FieldLengths.ArtifactTitle);

        builder.Property(x => x.Text);
               //.has HasMaxLength(DbConstants.FieldLengths.ArtifactTitle);

        // Indices
        builder.HasIndex(x => x.Title);        
    }
}
