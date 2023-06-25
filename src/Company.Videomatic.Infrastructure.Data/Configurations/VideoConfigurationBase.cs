namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class VideoConfigurationBase : IEntityTypeConfiguration<Video>
{
    public class FieldLengths
    {
        public const int Location = 1024;
        public const int Title = 500;
        //public const int Description = PlaylistConfigurationBase.FieldLengths.Description;
    }

    public virtual void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable("Videos");

        // Fields        
        builder.Property(x => x.Location)
               .HasMaxLength(FieldLengths.Location); 
        builder.Property(x => x.Title)
               .HasMaxLength(FieldLengths.Title);
        builder.Property(x => x.Description);
               //.HasMaxLength(FieldLengths.Description);


        // Relationships
        builder.HasMany(x => x.Thumbnails)            
               .WithOne()
               .HasForeignKey(x => x.VideoId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Transcripts)
               .WithOne()
               .HasForeignKey(x => x.VideoId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Artifacts)
               .WithOne()
               .HasForeignKey(x => x.VideoId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.VideoTags)
               .WithOne()
               .HasForeignKey(x => x.VideoId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

        // Indices
        builder.HasIndex(x => x.Location);
        builder.HasIndex(x => x.Title);
        //builder.HasIndex(x => x.Description); // 5000 chars is too long for an index
    }
}