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
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

        builder.Property(x => x.Location)
               .HasMaxLength(FieldLengths.Location); 

        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Title);
        
        builder.Property(x => x.Description);                
        //.HasMaxLength(FieldLengths.Description);

        var thumbnails = builder.OwnsMany(x => x.Thumbnails,
            (builder) => 
            {
                // See https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities#collections-of-owned-types
                builder.WithOwner().HasForeignKey("VideoId");
                builder.Property("Id");
                builder.HasKey("Id");

                builder.Property(x => x.Location).HasMaxLength(FieldLengths.Location);
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
           

        // Relationships
        //builder.HasMany(x => x.Thumbnails)            
        //       .WithOne()
        //       .HasForeignKey(x => x.VideoId)
        //       .IsRequired(true)
        //       .OnDelete(DeleteBehavior.Cascade);
        
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
        builder.HasIndex(x => x.Name);
        
    }
}