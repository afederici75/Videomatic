namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class ThumbnailDbConfiguration : ThumbnailConfigurationBase
{
    public override void Configure(EntityTypeBuilder<Thumbnail> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}