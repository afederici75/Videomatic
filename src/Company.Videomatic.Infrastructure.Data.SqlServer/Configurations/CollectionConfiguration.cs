namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class CollectionConfiguration : CollectionDbConfigurationBase
{
    public override void Configure(EntityTypeBuilder<VideoCollectionDb> builder)
    {
        base.Configure(builder);
        
        //builder.OverrideIEntityForSqlServer();
    }
}