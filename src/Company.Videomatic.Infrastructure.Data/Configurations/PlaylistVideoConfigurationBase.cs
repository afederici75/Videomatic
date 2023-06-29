namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class PlaylistVideoConfigurationBase : IEntityTypeConfiguration<PlaylistVideo>
{
    public static class FieldLengths
    {
        
    }

    public virtual void Configure(EntityTypeBuilder<PlaylistVideo> builder)
    {
        builder.ToTable("PlaylistVideos");

        // Fields
        builder.Property(x => x.VideoId)
               .HasConversion(x => x.Value, y => new VideoId(y));

        // Relationships
        //builder.HasMany(x => x.Videos)
        //       .WithMany(x => x.Tags);

        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}

