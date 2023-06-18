namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class TagDbConfiguration : TagDbConfigurationBase
{
    public override void Configure(EntityTypeBuilder<TagDb> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}
