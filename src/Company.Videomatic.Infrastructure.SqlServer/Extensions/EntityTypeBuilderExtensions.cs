using Company.SharedKernel.Abstractions;
using Company.Videomatic.Infrastructure.Data.SqlServer;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

internal static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<T> OverrideIEntityForSqlServer<T>(this EntityTypeBuilder<T> @this)
        where T : class, IEntity
    {
        @this.Property(nameof(IEntity.Id))
             .HasDefaultValueSql($"NEXT VALUE FOR {SqlServerVideomaticDbContext.SequenceName}");

        @this.HasIndex(x => x.Id)
             .IsUnique();

        return @this;
    }
}