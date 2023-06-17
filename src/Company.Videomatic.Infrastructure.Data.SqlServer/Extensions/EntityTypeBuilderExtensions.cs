
namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

internal static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<T> AddSequenceForId<T>(this EntityTypeBuilder<T> @this)
        where T : class, IEntity
    {        
        if (!SqlServerVideomaticDbContext.TryGetSequenceOfType(typeof(T), out var sequenceName))
            throw new InvalidOperationException($"Could not determine a name for the sequence of {nameof(IEntity)} {typeof(T).Name}.");

        @this.Property(nameof(IEntity.Id))
             .HasDefaultValueSql($"NEXT VALUE FOR {sequenceName}");        
    
        @this.HasIndex(x => x.Id)
             .IsUnique();
    
        return @this;
    }
}