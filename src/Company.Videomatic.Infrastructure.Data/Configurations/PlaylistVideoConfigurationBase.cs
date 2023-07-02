using Company.Videomatic.Domain.Entities.PlaylistAggregate;

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
        builder.HasKey(x => new { x.PlaylistId, x.VideoId });

        builder.Property(x => x.VideoId)
               .HasConversion(x => x.Value, y => y);

        builder.Property(x => x.PlaylistId)
               .HasConversion(x => x.Value, y => y);
      
    }
}

