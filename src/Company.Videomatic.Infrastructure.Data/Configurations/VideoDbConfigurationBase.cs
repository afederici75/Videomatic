namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class VideoDbConfigurationBase : IEntityTypeConfiguration<VideoDb>
{
    public virtual void Configure(EntityTypeBuilder<VideoDb> builder)
    {
        builder.ToTable("Videos");

        // Common
        builder.HasIndex(x => x.Id)
               .IsUnique();

        // Fields        
        //builder.Property(x => x.ProviderId)
        //       .HasMaxLength(VideomaticConstants.DbFieldLengths.ProviderId);
        //builder.Property(x => x.ProviderVideoId)
        //       .HasMaxLength(VideomaticConstants.DbFieldLengths.YTVideoId);
        builder.Property(x => x.Location)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.Url); 
        builder.Property(x => x.Title)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.YTVideoTitle);
        builder.Property(x => x.Description)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.YTVideoDescription);


        // Relationships
        builder.HasMany(x => x.Thumbnails)            
               .WithOne()
               //.IsRequired(true)
               .HasForeignKey("VideoId")
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Transcripts)
               .WithOne()
               //.IsRequired(true)
               .HasForeignKey("TranscriptId")
               //.HasPrincipalKey()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Artifacts)
               .WithOne()
               //.IsRequired(true)
               .HasForeignKey("VideoId")
               //.HasPrincipalKey()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Tags)
               .WithMany(x => x.Videos)
               .UsingEntity("TagsAndVideos");
        // TODO: Figure out how to change the name of the fields in the table TagsAndVideos: I get TagsId and VideosId instead of TagId and VideoId

        builder.HasMany(x => x.Playlists)
               .WithMany(x => x.Videos)
               .UsingEntity("PlatlistsAndVideos");
        // TODO: Figure out how to change the name of the fields in the table VideoCollectionsAndVideos. I get VideoCollectionsId and VideosId instead of VideoCollectionId and VideoId

        // Indices
        //builder.HasIndex(x => x.ProviderId);
        builder.HasIndex(x => x.Location);
        builder.HasIndex(x => x.Title);
        //builder.HasIndex(x => x.Description); // 5000 chars is too long for an index
    }
}