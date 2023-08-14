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

        // Fields
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Name);

        builder.Property(x => x.Description); // MAX

        var valueComparer = new ValueComparer<IEnumerable<string>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());

        builder.Property(x => x.Tags)
               .HasConversion(x => string.Join(',', x),
                              y => y.Split(',', StringSplitOptions.RemoveEmptyEntries).ToHashSet())
               .Metadata.SetValueComparer(valueComparer);
        
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
