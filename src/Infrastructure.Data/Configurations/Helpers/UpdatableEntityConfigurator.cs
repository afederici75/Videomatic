namespace Infrastructure.Data.Configurations.Helpers;

public class UpdatableEntityConfigurator<T, TId>  : EntityConfigurator<T, TId>
    where T : class, IUpdateableEntity
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.CreatedOn);
        //.ValueGeneratedOnAdd()
        //.HasDefaultValueSql("GETUTCDATE()")
        //.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        builder.Property(x => x.UpdatedOn);
    }

}
