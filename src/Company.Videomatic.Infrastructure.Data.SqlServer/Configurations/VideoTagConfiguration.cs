namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class VideoTagConfiguration : VideoTagConfigurationBase
{
    public override void Configure(EntityTypeBuilder<VideoTag> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}