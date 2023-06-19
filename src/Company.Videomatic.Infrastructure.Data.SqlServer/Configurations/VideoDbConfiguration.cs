namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class VideoDbConfiguration : VideoConfigurationBase
{
    public override void Configure(EntityTypeBuilder<Video> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}