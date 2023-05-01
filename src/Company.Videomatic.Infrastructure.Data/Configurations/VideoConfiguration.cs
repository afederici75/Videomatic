namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class VideoConfigurationBase : IEntityTypeConfiguration<Video>
{
    public virtual void Configure(EntityTypeBuilder<Video> builder)
    {
        // Common
        builder.ConfigureIEntity();

        // Fields        
        builder.Property(x => x.ProviderId)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.ProviderId);
        builder.Property(x => x.ProviderVideoId)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.YTVideoId);
        builder.Property(x => x.VideoUrl)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.Url); 
        builder.Property(x => x.Title)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.YTVideoTitle);
        builder.Property(x => x.Description)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.YTVideoDescription);


        // Relationships
        builder.HasMany(x => x.Thumbnails)
               .WithOne()
               //.IsRequired(true)
               //.HasForeignKey()
               //.HasPrincipalKey()
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Transcripts)
               .WithOne()
               //.IsRequired(true)
               //.HasForeignKey()
               //.HasPrincipalKey()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Artifacts)
               .WithOne()
               //.IsRequired(true)
               //.HasForeignKey()
               //.HasPrincipalKey()
               .OnDelete(DeleteBehavior.Cascade);

        // Indices
        builder.HasIndex(x => x.ProviderId);
        builder.HasIndex(x => x.VideoUrl);
        builder.HasIndex(x => x.Title);
        //builder.HasIndex(x => x.Description); // 5000 chars is too long for an index
    }
}