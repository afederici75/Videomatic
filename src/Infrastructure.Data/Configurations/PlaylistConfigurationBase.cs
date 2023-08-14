using Infrastructure.Data.Configurations.Helpers;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data.Configurations;



public abstract class PlaylistConfigurationBase : IEntityTypeConfiguration<Playlist>
{
    public const string TableName = "Playlists";

    public static class FieldLengths
    {
        public const int Name = 120;
    }

   

    public virtual void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable(TableName, VideomaticConstants.VideomaticSchema);
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

        new ImportedEntityConfigurator<PlaylistId, Playlist>().Configure(builder);

        // Relationships
        builder.HasMany(x => x.Videos)
               .WithOne()
               .HasForeignKey(nameof(PlaylistVideo.PlaylistId));
        
        builder.OwnsOne(x => x.Origin, EntityOriginConfigurator.Configure);
        
        builder.OwnsOne(x => x.Thumbnail, ThumbnailConfigurator.Configure);
        
        builder.OwnsOne(x => x.Picture, ThumbnailConfigurator.Configure);        
    }
}
