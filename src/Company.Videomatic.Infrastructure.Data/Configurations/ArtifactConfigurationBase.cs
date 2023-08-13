namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class ArtifactConfigurationBase : IEntityTypeConfiguration<Artifact>
{
    public const string TableName = "Artifacts";

    public static class FieldLengths
    { 
        public const int Title = 100;
        public const int Type = 128; // TODO: Could be way smaller
    }

    public virtual void Configure(EntityTypeBuilder<Artifact> builder)
    {        
        builder.ToTable(TableName, VideomaticConstants.VideomaticSchema);

        // Fields
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

        builder.Property(x => x.VideoId)
               .HasConversion(x => x.Value, y => new (y));
        
        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Title);

        builder.Property(x => x.Type)
               .HasMaxLength(FieldLengths.Type);

        builder.Property(x => x.Text); // MAX

        // Indices
        builder.HasIndex(x => x.Name);        
    }
}
