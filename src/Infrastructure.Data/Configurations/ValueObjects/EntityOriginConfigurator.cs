using Domain;

namespace Infrastructure.Data.Configurations.ValueObjects;

public class EntityOriginConfigurator
{
    static class OriginLengths
    {
        
    }

    public static void Configure<T>(OwnedNavigationBuilder<T, EntityOrigin> bld)
        where T : class
    {
        // ----- Fields ----- //
        bld.Property(x => x.ProviderId).HasMaxLength(FieldLengths.ExternalId);
        bld.Property(x => x.ProviderItemId).HasMaxLength(FieldLengths.ExternalId);
        bld.Property(x => x.ETag).HasMaxLength(FieldLengths.ETag);
        bld.Property(x => x.ChannelId).HasMaxLength(FieldLengths.ExternalId);
        bld.Property(x => x.ChannelName).HasMaxLength(FieldLengths.Name);
        bld.Property(x => x.Name).HasMaxLength(FieldLengths.Name);
        bld.Property(x => x.Description); // MAX
        bld.Property(x => x.PublishedOn);
        bld.Property(x => x.EmbedHtml).HasMaxLength(FieldLengths.EmbedHtml);
        bld.Property(x => x.DefaultLanguage).HasMaxLength(FieldLengths.DefaultLanguage);

        // ----- Relationships ----- //
        bld.OwnsOne(x => x.Thumbnail, ImageReferenceConfigurator.Configure);
        bld.OwnsOne(x => x.Picture, ImageReferenceConfigurator.Configure);

        // ----- Indices ----- //
        bld.HasIndex(x => x.ProviderId);
        bld.HasIndex(x => x.ProviderItemId);
        bld.HasIndex(x => x.ETag);
        bld.HasIndex(x => x.ChannelId);
        bld.HasIndex(x => x.ChannelName);
        bld.HasIndex(x => x.Name);
        bld.HasIndex(x => x.DefaultLanguage);
    }
}