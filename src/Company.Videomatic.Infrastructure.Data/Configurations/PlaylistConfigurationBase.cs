using Company.Videomatic.Domain;

namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class PlaylistConfigurationBase : IEntityTypeConfiguration<Playlist>
{
    public const string TableName = "Playlists";

    public static class FieldLengths
    {
        public const int Name = 120;
    }

   

    public virtual void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable(TableName);

        // Fields
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Name);

        builder.Property(x => x.Description); // MAX

        builder.Property(x => x.Tags)
            .HasConversion(x => string.Join(',', x),
                           y => y.Split(',', StringSplitOptions.RemoveEmptyEntries).ToHashSet());
        // Relationships
        builder.HasMany(x => x.Videos)
               .WithOne()
               .HasForeignKey(nameof(PlaylistVideo.PlaylistId));

        builder.OwnsOne(x => x.Origin, EntityOriginConfigurator.Configure);

        builder.OwnsOne(x => x.Thumbnail, ThumbnailConfigurator.Configure);

        builder.OwnsOne(x => x.Picture, ThumbnailConfigurator.Configure);

        // Indices
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Description);
    }
}

public static class Lengths
{
    public static class Generic
    {
        public const int Url = 1024;
        public const int Name = 120;
        public const int ExternalId = 50;
    }   
}

public static class ThumbnailConfigurator
{
    public static void Configure<T>(OwnedNavigationBuilder<T, Thumbnail> bld)
        where T : class
    {
        bld.Property(x => x.Location).HasMaxLength(Lengths.Generic.Url);
        bld.Property(x => x.Width);
        bld.Property(x => x.Height);
    }
}   

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
        bld.Property(x => x.ProviderId).HasMaxLength(Lengths.Generic.ExternalId);
        bld.Property(x => x.ProviderItemId).HasMaxLength(Lengths.Generic.ExternalId);
        bld.Property(x => x.ETag).HasMaxLength(OriginLengths.ETag);
        bld.Property(x => x.ChannelId).HasMaxLength(Lengths.Generic.ExternalId);
        bld.Property(x => x.ChannelName).HasMaxLength(Lengths.Generic.Name);
        bld.Property(x => x.Name).HasMaxLength(Lengths.Generic.Name);
        bld.Property(x => x.Description); // MAX
        bld.Property(x => x.PublishedOn);
        bld.Property(x => x.EmbedHtml).HasMaxLength(OriginLengths.EmbedHtml);
        bld.Property(x => x.DefaultLanguage).HasMaxLength(OriginLengths.DefaultLanguage);
        
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
