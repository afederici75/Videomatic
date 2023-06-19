namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class PlaylistDbConfiguration : PlaylistConfigurationBase
{
    public override void Configure(EntityTypeBuilder<Playlist> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}
