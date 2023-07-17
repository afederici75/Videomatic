namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class PlaylistConfigurationBase : IEntityTypeConfiguration<Playlist>
{
    public static class FieldLengths
    {
        public const int Name = 120;
        //public const int Description = 4096;
    }

    public virtual void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable("Playlists");

        // Fields
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Name);

        builder.Property(x => x.Description);
        
        // Relationships
        builder.HasMany(x => x.Videos)
               .WithOne()
               .HasForeignKey(nameof(PlaylistVideo.PlaylistId));

        // Indices
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Description);
    }
}
