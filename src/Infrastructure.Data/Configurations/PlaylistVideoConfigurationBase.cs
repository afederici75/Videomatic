namespace Infrastructure.Data.Configurations;

public abstract class PlaylistVideoConfigurationBase : IEntityTypeConfiguration<PlaylistVideo>
{
    private const string TableName = "PlaylistVideos";

    public static class FieldLengths
    {
        
    }

    public virtual void Configure(EntityTypeBuilder<PlaylistVideo> builder)
    {
        builder.ToTable(TableName, VideomaticConstants.VideomaticSchema);

        // Fields
        builder.HasKey(x => new { x.PlaylistId, x.VideoId });

        builder.Property(x => x.VideoId)
               .HasConversion(x => (int)x, y => y);

        builder.Property(x => x.PlaylistId)
               .HasConversion(x => (int)x, y => y);
      
    }
}

