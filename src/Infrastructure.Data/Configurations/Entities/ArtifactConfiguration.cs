namespace Infrastructure.Data.Configurations.Entities;

public abstract class ArtifactConfiguration : TrackedEntityConfiguration<Artifact>,
    IEntityTypeConfiguration<Artifact>
{
    public override void Configure(EntityTypeBuilder<Artifact> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => new ArtifactId(y))
               .IsRequired(true);

        // ----- Fields ----- //
        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Title);

        builder.Property(x => x.Type)
               .HasMaxLength(FieldLengths.Type);

        builder.Property(x => x.Text); // MAX

        // ----- Indices ----- //
        builder.HasIndex(x => x.Name);
    }
}
