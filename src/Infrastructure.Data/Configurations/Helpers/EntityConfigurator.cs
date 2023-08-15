namespace Infrastructure.Data.Configurations.Helpers;

public class EntityConfigurator<T, TId> : IEntityTypeConfiguration<T>
    where T : class, IEntity
{ 
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        //builder.Property(x => x.Id)
        //       .ValueGeneratedOnAdd()
        //       .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        //
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
