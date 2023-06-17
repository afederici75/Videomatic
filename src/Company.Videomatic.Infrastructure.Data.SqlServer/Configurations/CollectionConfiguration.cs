namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class CollectionConfiguration : VideoCollectionDbConfigurationBase
{
    public override void Configure(EntityTypeBuilder<VideoCollectionDb> builder)
    {
        base.Configure(builder);
        
        //builder.OverrideIEntityForSqlServer();
    }
}