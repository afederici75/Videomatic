using Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data.Configurations.Helpers;

public class ImportedEntityConfigurator<TId, T> : UpdatableEntityConfigurator<T>
    where T : ImportedEntity<TId>    
{
    public class FieldLengths
    {
        public const int URL = 1024;
        public const int Title = 500;
        public const int TagName = 100;
        //public const int Description = PlaylistConfigurationBase.FieldLengths.Description;
    }

    public override void Configure(EntityTypeBuilder<T> builder)        
    {
        base.Configure(builder);

        // Fields
        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Title);

        builder.Property(x => x.Description);

        // Tags
        var valueComparer = new ValueComparer<IEnumerable<string>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());

        builder.Property(x => x.Tags)
               .HasConversion(x => string.Join(',', x),
                              y => y.Split(',', StringSplitOptions.RemoveEmptyEntries).ToHashSet())

               .Metadata.SetValueComparer(valueComparer);

        // ---------- Owned Types ----------

        builder.OwnsOne(x => x.Thumbnail, ThumbnailConfigurator.Configure);

        builder.OwnsOne(x => x.Picture, ThumbnailConfigurator.Configure);

        var details = builder.OwnsOne(x => x.Origin, EntityOriginConfigurator.Configure);

        // ---------- Indices ----------
        builder.HasIndex(x => x.Name);
    }
}   