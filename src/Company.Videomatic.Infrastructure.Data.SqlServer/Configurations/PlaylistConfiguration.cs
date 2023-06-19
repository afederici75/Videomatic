namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class PlaylistConfiguration : PlaylistConfigurationBase
{
    public override void Configure(EntityTypeBuilder<Playlist> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}
