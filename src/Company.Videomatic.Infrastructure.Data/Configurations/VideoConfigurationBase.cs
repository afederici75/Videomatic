using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Transcript;

namespace Company.Videomatic.Infrastructure.Data.Configurations;

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
        builder.ToTable(TableName);

        // Fields
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

        //builder.Property(x => x.Location)
        //       .HasMaxLength(FieldLengths.URL); 

        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Title);
        
        builder.Property(x => x.Description);

        builder.Property(x => x.Tags)
            .HasConversion(x => string.Join(',', x),
                           y => y.Split(',', StringSplitOptions.RemoveEmptyEntries).ToHashSet());        

        #region Owned Types

        builder.OwnsOne(x => x.Thumbnail, ThumbnailConfigurator.Configure); 

        builder.OwnsOne(x => x.Picture, ThumbnailConfigurator.Configure);        

        var details = builder.OwnsOne(x => x.Origin, EntityOriginConfigurator.Configure);

        #endregion


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


        // Indices
        builder.HasIndex(x => x.Name);
        
    }
}