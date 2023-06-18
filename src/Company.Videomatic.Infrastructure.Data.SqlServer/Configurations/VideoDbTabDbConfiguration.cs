namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class VideoDbTabDbConfiguration : VideoDbTagDbConfigurationBase
{
    public override void Configure(EntityTypeBuilder<VideoDbTagDb> builder)
    {
        base.Configure(builder);
    }
}