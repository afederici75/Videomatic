namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<T> ConfigureIEntity<T>(this EntityTypeBuilder<T> @this)
        where T : class, IEntity
    {
        @this.Property(nameof(IEntity.Id));

        @this.HasIndex(x => x.Id)
             .IsUnique();

        return @this;
    }
}