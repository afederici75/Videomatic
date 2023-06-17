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
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Transcripts)
               .WithOne()
               .HasForeignKey("TranscriptId")
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Artifacts)
               .WithOne()
               .HasForeignKey("VideoId")
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
        builder.HasIndex(x => x.Location);
        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.Description); // 5000 chars is too long for an index
    }
}