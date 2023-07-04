namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class PlaylistVideoConfiguration : PlaylistVideoConfigurationBase
{
    public override void Configure(EntityTypeBuilder<VideoPlaylist> builder)
    {
        base.Configure(builder);
        
    }
}