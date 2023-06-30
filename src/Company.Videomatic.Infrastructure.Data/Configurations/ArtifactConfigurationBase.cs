namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class ArtifactConfigurationBase : IEntityTypeConfiguration<Artifact>
{
    public static class FieldLengths
    { 
        public const int Title = 450;
        public const int Type = 128;    
    }

    public virtual void Configure(EntityTypeBuilder<Artifact> builder)
    {        
        builder.ToTable("Artifacts");

        // Fields
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

        builder.Property(x => x.VideoId)
               .HasConversion(x => x.Value, y => new VideoId(y));
        
        builder.Property(x => x.Title)
               .HasMaxLength(FieldLengths.Title);

        builder.Property(x => x.Type)
               .HasMaxLength(FieldLengths.Type);

        builder.Property(x => x.Text); // MAX

        // Indices
        builder.HasIndex(x => x.Title);        
    }
}
