namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class EntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        // Fields
        builder.Property(x => x.Id);
        //.HasDefaultValueSql($"NEXT VALUE FOR {DbConstants.SequenceName}");
    }
}