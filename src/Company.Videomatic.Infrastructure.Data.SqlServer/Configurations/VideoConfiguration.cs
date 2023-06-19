namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class VideoConfiguration : VideoConfigurationBase
{
    public override void Configure(EntityTypeBuilder<Video> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}