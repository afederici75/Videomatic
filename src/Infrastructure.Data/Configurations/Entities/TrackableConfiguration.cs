using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Data.Configurations.Entities;

public abstract class TrackableConfiguration<T> : IEntityTypeConfiguration<T>
    where T : class, ITrackable
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        // ---------- Fields ----------
        builder.Property(x => x.CreatedOn)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETUTCDATE()")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        builder.Property(x => x.UpdatedOn)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("GETUTCDATE()")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        // ---------- Indices ----------
        builder.HasIndex(x => x.CreatedOn);
        builder.HasIndex(x => x.UpdatedOn);
    }
}