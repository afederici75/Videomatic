using Company.Videomatic.Infrastructure.Data.Extensions;

namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class ArtifactConfigurationBase : IEntityTypeConfiguration<Artifact>
{
    public void Configure(EntityTypeBuilder<Artifact> builder)
    {
        // Common
        builder.ConfigureIEntity();

        // Fields        
        builder.Property(x => x.Title)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.ArtifactTitle);

        builder.Property(x => x.Text);
               //.has HasMaxLength(DbConstants.FieldLengths.ArtifactTitle);

        // Indices
        builder.HasIndex(x => x.Title);        
    }
}
