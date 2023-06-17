
namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<T> AddSequenceForId<T>(this EntityTypeBuilder<T> @this)
        where T : class, IEntity
    {
        @this.Property(nameof(IEntity.Id))
             .HasDefaultValueSql($"NEXT VALUE FOR {SqlServerVideomaticDbContext.SequenceName}");
    
        @this.HasIndex(x => x.Id)
             .IsUnique();
    
        return @this;
    }
}