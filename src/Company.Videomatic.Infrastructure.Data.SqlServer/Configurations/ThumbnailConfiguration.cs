namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class ThumbnailConfiguration : ThumbnailDbConfigurationBase
{
    public override void Configure(EntityTypeBuilder<ThumbnailDb> builder)
    {
        base.Configure(builder);

        //builder.OverrideIEntityForSqlServer();
    }
}