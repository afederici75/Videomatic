namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class TagDbConfiguration : TagConfigurationBase
{
    public override void Configure(EntityTypeBuilder<TagDb> builder)
    {
        base.Configure(builder);

        builder.AddSequenceForId();
    }
}   
