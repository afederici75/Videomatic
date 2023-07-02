using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Aggregates.Video;

namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class VideoConfigurationBase : IEntityTypeConfiguration<Video>
{
    public class FieldLengths
    {
        public const int Location = 1024;
        public const int Title = 500;
        public const int TagName = 35;
        //public const int Description = PlaylistConfigurationBase.FieldLengths.Description;
    }

    public virtual void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable("Videos");

        // Fields
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

        builder.Property(x => x.Location)
               .HasMaxLength(FieldLengths.Location); 

        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Title);
        
        builder.Property(x => x.Description);
        //.HasMaxLength(FieldLengths.Description);

        builder.HasMany(typeof(PlaylistVideo))
               .WithOne()
               .HasForeignKey("VideoId");

        #region Owned Types
        var thumbnails = builder.OwnsMany(x => x.Thumbnails,
            (builder) => 
            {
                // See https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities#collections-of-owned-types
                builder.WithOwner().HasForeignKey("VideoId");
                builder.Property("Id");
                builder.HasKey("Id");
                
                builder.Property(x => x.Location).HasMaxLength(FieldLengths.Location);
            });

        var tags = builder.OwnsMany(x => x.VideoTags,
           (builder) =>
           {
               // See https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities#collections-of-owned-types
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
            //builder.Property(x => x.VideoPublishedAt);
            builder.Property(x => x.ChannelId).HasMaxLength(TempSafeLength);
            builder.Property(x => x.PlaylistId).HasMaxLength(TempSafeLength);
            builder.Property(x => x.Provider).HasMaxLength(TempSafeLength);
            builder.Property(x => x.VideoOwnerChannelTitle).HasMaxLength(TempSafeLength);
            builder.Property(x => x.VideoOwnerChannelId).HasMaxLength(TempSafeLength);

            builder.HasIndex(x => x.VideoOwnerChannelId);
            builder.HasIndex(x => x.ChannelId);
            builder.Property(x => x.VideoPublishedAt);
            builder.Property(x => x.PlaylistId);
            builder.Property(x => x.Position);
        });

        #endregion


        builder.HasMany(typeof(Artifact))
               .WithOne()
               .HasForeignKey("VideoId")
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(typeof(Transcript))
               .WithOne()
               .HasForeignKey("VideoId")
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);



        // Indices
        builder.HasIndex(x => x.Location);
        builder.HasIndex(x => x.Name);
        
    }
}