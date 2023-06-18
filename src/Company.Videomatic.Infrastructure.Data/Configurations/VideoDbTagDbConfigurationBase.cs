namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class VideoDbTagDbConfigurationBase : IEntityTypeConfiguration<VideoDbTagDb>
{
    public virtual void Configure(EntityTypeBuilder<VideoDbTagDb> builder)
    {
        builder.ToTable("VideoTags");

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

