namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class PlaylistDbConfiguration : PlaylistDbConfigurationBase
{
    public override void Configure(EntityTypeBuilder<PlaylistDb> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}