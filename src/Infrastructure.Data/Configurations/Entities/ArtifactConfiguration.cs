namespace Infrastructure.Data.Configurations.Entities;

public abstract class ArtifactConfiguration : TrackedEntityConfiguration<Artifact>,
    IEntityTypeConfiguration<Artifact>
{
    public const string TableName = "Artifacts";

    public static class FieldLengths
    {
        public const int Title = 100;
        public const int Type = 128; // TODO: Could be way smaller
    }

    public override void Configure(EntityTypeBuilder<Artifact> builder)
    {
        base.Configure(builder);

        // ----- Table ----- //
        builder.ToTable(TableName, VideomaticConstants.VideomaticSchema);

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
