using Domain;

namespace Infrastructure.Data.Configurations.Helpers;

public class EntityOriginConfigurator
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
        bld.OwnsOne(x => x.Thumbnail, ImageReferenceConfigurator.Configure);
        bld.OwnsOne(x => x.Picture, ImageReferenceConfigurator.Configure);

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
