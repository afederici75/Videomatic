using Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data.Configurations.Helpers;

public static class EntityOriginConfigurator
{
    static class OriginLengths
    {
        public const int ETag = 50;

        public const int EmbedHtml = 2048;
        public const int DefaultLanguage = 10;
    }

    public static void Configure<T>(OwnedNavigationBuilder<T, EntityOrigin> bld)
        where T : class
    {
        // Properties
        bld.Property(x => x.ProviderId).HasMaxLength(FieldLengths.Generic.ExternalId);
        bld.Property(x => x.ProviderItemId).HasMaxLength(FieldLengths.Generic.ExternalId);
        bld.Property(x => x.ETag).HasMaxLength(OriginLengths.ETag);
        bld.Property(x => x.ChannelId).HasMaxLength(FieldLengths.Generic.ExternalId);
        bld.Property(x => x.ChannelName).HasMaxLength(FieldLengths.Generic.Name);
        bld.Property(x => x.Name).HasMaxLength(FieldLengths.Generic.Name);
        bld.Property(x => x.Description); // MAX
        bld.Property(x => x.PublishedOn);
        bld.Property(x => x.EmbedHtml).HasMaxLength(OriginLengths.EmbedHtml);
        bld.Property(x => x.DefaultLanguage).HasMaxLength(OriginLengths.DefaultLanguage);

        // Relationships
        bld.OwnsOne(x => x.Thumbnail, ThumbnailConfigurator.Configure);
        bld.OwnsOne(x => x.Picture, ThumbnailConfigurator.Configure);

        // Indices
        bld.HasIndex(x => x.ProviderId);
        bld.HasIndex(x => x.ProviderItemId);
        bld.HasIndex(x => x.ETag);
        bld.HasIndex(x => x.ChannelId);
        bld.HasIndex(x => x.ChannelName);
        bld.HasIndex(x => x.Name);
        bld.HasIndex(x => x.DefaultLanguage);
    }
}

public static class EntityConfigurator 
{ 
    public static void Configure<T>(EntityTypeBuilder<T> builder) 
        where T : class, IEntity
    {
        builder.Property(x => x.Id) 
               .HasConversion(x => (int)x, y => y);        
    }
}

public static class ImportedEntityConfigurator<TId, T> 
    where T : ImportedEntity<TId>    
{
    public class FieldLengths
    {
        public const int URL = 1024;
        public const int Title = 500;
        public const int TagName = 100;
        //public const int Description = PlaylistConfigurationBase.FieldLengths.Description;
    }

    public static void Configure(EntityTypeBuilder<T> builder)        
    {
        EntityConfigurator.Configure(builder);

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