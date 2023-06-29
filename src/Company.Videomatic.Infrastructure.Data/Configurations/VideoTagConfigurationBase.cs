namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class VideoTagConfigurationBase : IEntityTypeConfiguration<VideoTag>
{
    public class FieldLengths
    {
        public const int Name = 100;        
    }

    public virtual void Configure(EntityTypeBuilder<VideoTag> builder)
    {
        builder.ToTable("VideoTags");

        // Fields
        builder.Property(x => x.VideoId)
               .HasConversion(x => x.Value, y => new VideoId(y))
               .IsRequired(true);

        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Name);
        //
        //// Relationships
        //builder.HasMany(x => x.Videos)
        //       .WithMany(x => x.Tags);
        //
        //// Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}

