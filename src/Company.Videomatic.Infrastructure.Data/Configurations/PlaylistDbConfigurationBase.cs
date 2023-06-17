namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class PlaylistDbConfigurationBase : IEntityTypeConfiguration<PlaylistDb>
{
    public virtual void Configure(EntityTypeBuilder<PlaylistDb> builder)
    {
        builder.ToTable("Playlists");

        // Common
        builder.HasIndex(x => x.Id)
               .IsUnique();

        // Fields
        builder.Property(x => x.Name)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.CollectionName);
        
        // Relationships
        builder.HasMany(x => x.Videos)
               .WithMany(x => x.Playlists);
        
        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}
