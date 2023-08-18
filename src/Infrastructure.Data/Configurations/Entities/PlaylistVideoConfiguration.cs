namespace Infrastructure.Data.Configurations.Entities;

public abstract class PlaylistVideoConfiguration : IEntityTypeConfiguration<PlaylistVideo>
{
    public virtual void Configure(EntityTypeBuilder<PlaylistVideo> builder)
    {
        // Fields
        builder.HasKey(x => new { x.PlaylistId, x.VideoId });

        builder.Property(x => x.VideoId)
               .HasConversion(x => (int)x, y => (VideoId)y);

        builder.Property(x => x.PlaylistId)
               .HasConversion(x => (int)x, y => (PlaylistId)y);

    }
}

