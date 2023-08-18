using Infrastructure.Data.Configurations.Entities;

namespace Infrastructure.Data.SqlServer.Configurations;

public class SqlServerPlaylistVideoConfiguration : PlaylistVideoConfiguration
{
    public const string TableName = "PlaylistVideos";

    public override void Configure(EntityTypeBuilder<PlaylistVideo> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName, Constants.VideomaticDbSchema);

        builder.HasKey(x => new { x.PlaylistId, x.VideoId });
               
    }
}