using Infrastructure.Data.Configurations.Entities;

namespace Infrastructure.Data.SqlServer.Configurations;

public class SqlServerPlaylistVideoConfiguration : PlaylistVideoConfiguration
{
    public override void Configure(EntityTypeBuilder<PlaylistVideo> builder)
    {
        base.Configure(builder);
        
    }
}