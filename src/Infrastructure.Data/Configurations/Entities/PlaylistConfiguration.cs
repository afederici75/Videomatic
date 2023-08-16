using Infrastructure.Data.Configurations.ValueObjects;

namespace Infrastructure.Data.Configurations.Entities;

public abstract class PlaylistConfiguration : ImportedEntityConfiguration<Playlist>,
    IEntityTypeConfiguration<Playlist>
{
    public const string TableName = "Playlists";

    public override void Configure(EntityTypeBuilder<Playlist> builder)
    {
        base.Configure(builder);

        // ----- Table ----- //
        builder.ToTable(TableName, VideomaticConstants.VideomaticSchema);

        // ----- Fields ----- //        
        builder.HasMany(x => x.Videos)
               .WithOne()
               .HasForeignKey(nameof(PlaylistVideo.PlaylistId));

        builder.OwnsOne(x => x.Origin, EntityOriginConfigurator.Configure);

        builder.OwnsOne(x => x.Thumbnail, ImageReferenceConfigurator.Configure);

        builder.OwnsOne(x => x.Picture, ImageReferenceConfigurator.Configure);

        // ----- Indices ----- //
    }
}
