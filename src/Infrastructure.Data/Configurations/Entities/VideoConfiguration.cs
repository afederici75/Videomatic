using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data.Configurations.Entities;

public abstract class VideoConfiguration : ImportedEntityConfiguration<Video>,
    IEntityTypeConfiguration<Video>
{    
    public override void Configure(EntityTypeBuilder<Video> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => (VideoId)y);

        // ---------- Relationships ----------

        builder.HasMany(typeof(Artifact))
               .WithOne()
               .HasForeignKey(nameof(Artifact.VideoId))
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(typeof(Transcript))
               .WithOne()
               .HasForeignKey(nameof(Transcript.VideoId))
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(typeof(PlaylistVideo))
               .WithOne()
               .HasForeignKey(nameof(PlaylistVideo.VideoId));

        // ---------- Indices ----------
    }
}