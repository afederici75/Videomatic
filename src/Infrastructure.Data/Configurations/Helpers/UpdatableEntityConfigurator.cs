namespace Infrastructure.Data.Configurations.Helpers;

public class EntityConfigurator<T>
    where T : class
{ 

}

public class UpdatableEntityConfigurator<T> 
    where T : class, IUpdateableEntity
{ 
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        //builder.Property(x => x.CreatedOn)
        //       .ValueGeneratedOnAdd()
        //       .HasDefaultValueSql("GETUTCDATE()")
        //       .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        //
        //builder.Property(x => x.UpdatedOn)
        //       .ValueGeneratedOnAddOrUpdate()
        //       .HasDefaultValueSql("GETUTCDATE()")
        //       .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
    }
}
