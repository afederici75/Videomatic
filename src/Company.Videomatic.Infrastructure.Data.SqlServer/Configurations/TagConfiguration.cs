namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class TagConfiguration : TagConfigurationBase
{
    public override void Configure(EntityTypeBuilder<TagDb> builder)
    {
        base.Configure(builder);
        
        //builder.OverrideIEntityForSqlServer();
    }
}   
