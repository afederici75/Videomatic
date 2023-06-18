namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class PlaylistDbVideoDbConfigurationBase : IEntityTypeConfiguration<PlaylistDbVideoDb>
{
    public virtual void Configure(EntityTypeBuilder<PlaylistDbVideoDb> builder)
    {
        builder.ToTable("PlaylistVideos");

        // Fields
        //builder.Property(x => x.Name)
        //       .HasMaxLength(FieldLengths.Name);
        //
        //// Relationships
        //builder.HasMany(x => x.Videos)
        //       .WithMany(x => x.Tags);
        //
        //// Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}

