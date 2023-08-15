using Infrastructure.Data.Configurations.Helpers;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data.Configurations;

public abstract class VideoConfigurationBase : ImportedEntityConfigurationBase<Video>, 
    IEntityTypeConfiguration<Video>
{
    public const string TableName = "Videos";
    public const string TableNameForThumbnails = "Thumbnails";
    public const string TableNameForTags = "VideoTags";
   
    public override void Configure(EntityTypeBuilder<Video> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName, VideomaticConstants.VideomaticSchema);

        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

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