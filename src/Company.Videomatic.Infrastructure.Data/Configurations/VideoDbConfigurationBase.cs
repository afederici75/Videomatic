using Company.Videomatic.Infrastructure.Data.Model;

namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class VideoDbConfigurationBase : IEntityTypeConfiguration<VideoDb>
{
    public class FieldLengths
    {
        public const int Location = 1024;
        public const int Title = 500;
        public const int Description = PlaylistDbConfigurationBase.FieldLengths.Description;
    }

    public virtual void Configure(EntityTypeBuilder<VideoDb> builder)
    {
        builder.ToTable("Videos");

        // Fields        
        builder.Property(x => x.Location)
               .HasMaxLength(FieldLengths.Location); 
        builder.Property(x => x.Title)
               .HasMaxLength(FieldLengths.Title);
        builder.Property(x => x.Description)
               .HasMaxLength(FieldLengths.Description);


        // Relationships
        builder.HasMany(x => x.Thumbnails)            
               .WithOne()
               .HasForeignKey("VideoId")
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Transcripts)
               .WithOne()
               .HasForeignKey("VideoId")
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Artifacts)
               .WithOne()
               .HasForeignKey("VideoId")
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Tags)
               .WithMany(x => x.Videos)
               .UsingEntity<VideoDbTagDb>(
                l => l.HasOne<TagDb>(x => x.Tag).WithMany(x => x.VideoTags).HasForeignKey(x => x.TagId),
                r => r.HasOne<VideoDb>(x => x.Video).WithMany(x=> x.VideoTags).HasForeignKey(x => x.VideoId)
            );
                
        // Indices
        builder.HasIndex(x => x.Location);
        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.Description); // 5000 chars is too long for an index
    }
}