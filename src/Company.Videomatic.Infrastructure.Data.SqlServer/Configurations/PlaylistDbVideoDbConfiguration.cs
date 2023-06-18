namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class PlaylistDbVideoDbConfiguration : PlaylistDbVideoDbConfigurationBase
{
    public override void Configure(EntityTypeBuilder<PlaylistDbVideoDb> builder)
    {
        base.Configure(builder);
        
    }
}