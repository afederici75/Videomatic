namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class ThumbnailDbConfigurationBase : IEntityTypeConfiguration<ThumbnailDb>
{
    public static class FieldLengths
    {
        public const int Location = 1024;
    }

    public virtual void Configure(EntityTypeBuilder<ThumbnailDb> builder)
    {
        builder.ToTable("Thumbnails");

        // Fields
        builder.Property(x => x.Location)
               .HasMaxLength(FieldLengths.Location);

        // Indices
        builder.HasIndex(x => x.Resolution);
        builder.HasIndex(x => x.Location);
        builder.HasIndex(x => x.Height);
        builder.HasIndex(x => x.Width);
    }
}