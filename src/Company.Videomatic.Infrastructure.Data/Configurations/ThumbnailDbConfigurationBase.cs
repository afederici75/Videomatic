namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class ThumbnailDbConfigurationBase : IEntityTypeConfiguration<ThumbnailDb>
{
    public virtual void Configure(EntityTypeBuilder<ThumbnailDb> builder)
    {
        builder.ToTable("Thumbnails");

        // Common
        builder.HasIndex(x => x.Id)
               .IsUnique();

        // Fields        
        builder.Property(x => x.Location)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.Url);               

        // Indices
        builder.HasIndex(x => x.Resolution);
        builder.HasIndex(x => x.Location);
        builder.HasIndex(x => x.Height);
        builder.HasIndex(x => x.Width);
    }
}
