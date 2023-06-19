namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class VideoDbTabDbConfiguration : VideoTagConfigurationBase
{
    public override void Configure(EntityTypeBuilder<VideoTag> builder)
    {
        base.Configure(builder);
    }
}