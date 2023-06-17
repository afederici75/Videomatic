namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class PlaylistDbConfigurationBase : IEntityTypeConfiguration<PlaylistDb>
{
    public static class FieldLengths
    {
        public const int Name = 120;
        public const int Description = 2048;
    }

    public virtual void Configure(EntityTypeBuilder<PlaylistDb> builder)
    {
        builder.ToTable("Playlists");

        // Fields
        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Name);

        builder.Property(x => x.Description)
               .HasMaxLength(FieldLengths.Description);

        // Relationships
        builder.HasMany(x => x.Videos)
               .WithMany(x => x.Playlists);
        
        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}
