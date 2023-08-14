using Infrastructure.Data.Configurations.Helpers;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data.Configurations;

public abstract class VideoConfigurationBase : IEntityTypeConfiguration<Video>
{
    public const string TableName = "Videos";
    public const string TableNameForThumbnails = "Thumbnails";
    public const string TableNameForTags = "VideoTags";

    public class FieldLengths
    {
        public const int URL = 1024;
        public const int Title = 500;
        public const int TagName = 100;
        //public const int Description = PlaylistConfigurationBase.FieldLengths.Description;
    }

    public virtual void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable(TableName, VideomaticConstants.VideomaticSchema);
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

        new ImportedEntityConfigurator<VideoId, Video>().Configure(builder);

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