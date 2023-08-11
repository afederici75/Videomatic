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

        builder.Property(x => x.Location)
               .HasMaxLength(FieldLengths.URL); 

        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Title);
        
        builder.Property(x => x.Description);

        builder.Property(x => x.Thumbnail)
               .HasMaxLength(FieldLengths.URL);

        builder.Property(x => x.Picture)
               .HasMaxLength(FieldLengths.URL);

        #region Owned Types
        builder.OwnsOne(x => x.Thumbnail, b => 
        { 
            b.Property(x => x.Location).HasMaxLength(FieldLengths.URL);            
        });

        builder.OwnsOne(x => x.Picture, b =>
        {
            b.Property(x => x.Location).HasMaxLength(FieldLengths.URL);
        });

        //var thumbnails = builder.OwnsMany(x => x.Thumbnails,
        //    (builder) => 
        //    {
        //        builder.ToTable(TableNameForThumbnails);
        //
        //        // See https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities#collections-of-owned-types
        //        // Shadow properties
        //        builder.WithOwner().HasForeignKey("VideoId");
        //        builder.Property("Id");
        //        builder.HasKey("Id");
        //        
        //        builder.Property(x => x.Location)
        //               .HasMaxLength(FieldLengths.URL);
        //    });

        var tags = builder.OwnsMany(x => x.Tags,
           (builder) =>
           {
               builder.ToTable(TableNameForTags);

               // See https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities#collections-of-owned-types
               // Shadow properties
               builder.WithOwner().HasForeignKey("VideoId");
               builder.Property("Id");
               builder.HasKey("Id");
               // No Dups the video
               builder.HasIndex(nameof(VideoTag.Name), "VideoId")
                      .IsUnique(true);

               //
               builder.Property(x => x.Name).HasMaxLength(FieldLengths.TagName);
           });

        var details = builder.OwnsOne(x => x.Details, (builder) => 
        {
            const int TempSafeLength = 100;
            
            builder.Property(x => x.ProviderVideoId).HasMaxLength(TempSafeLength);
            builder.Property(x => x.Provider).HasMaxLength(TempSafeLength);
            builder.Property(x => x.VideoOwnerChannelTitle).HasMaxLength(TempSafeLength);
            builder.Property(x => x.VideoOwnerChannelId).HasMaxLength(TempSafeLength);
            builder.Property(x => x.VideoPublishedAt);
            builder.Property(x => x.ProviderVideoId);

            // Indices
            builder.HasIndex(x => x.ProviderVideoId);
            builder.HasIndex(x => x.Provider);
            builder.HasIndex(x => x.VideoOwnerChannelTitle);
            builder.HasIndex(x => x.VideoOwnerChannelId);
            builder.HasIndex(x => x.VideoPublishedAt);
            builder.HasIndex(x => x.ProviderVideoId);
        });

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
        builder.HasIndex(x => x.Location);
        builder.HasIndex(x => x.Name);
        
    }
}