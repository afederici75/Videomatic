namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class ThumbnailDbConfiguration : ThumbnailDbConfigurationBase
{
    public override void Configure(EntityTypeBuilder<ThumbnailDb> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}