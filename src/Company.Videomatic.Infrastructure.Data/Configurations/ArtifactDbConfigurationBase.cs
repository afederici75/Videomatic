namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class ArtifactDbConfigurationBase : IEntityTypeConfiguration<ArtifactDb>
{
    public virtual void Configure(EntityTypeBuilder<ArtifactDb> builder)
    {        
        builder.ToTable("Artifacts");

        // Common
        builder.HasIndex(x => x.Id)
               .IsUnique();

        // Fields        
        builder.Property(x => x.Title)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.ArtifactTitle);

        builder.Property(x => x.Text);
               //.has HasMaxLength(DbConstants.FieldLengths.ArtifactTitle);

        // Indices
        builder.HasIndex(x => x.Title);        
    }
}
