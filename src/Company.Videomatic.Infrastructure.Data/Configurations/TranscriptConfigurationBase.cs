using Company.Videomatic.Domain.Entities.TranscriptAggregate;

namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TranscriptConfigurationBase : IEntityTypeConfiguration<Transcript>
{
    public static class FieldLengths
    {
        public const int Language = 2;
    }

    public virtual void Configure(EntityTypeBuilder<Transcript> builder)
    {
        builder.ToTable("Transcripts");        
        
        // Fields
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => new TranscriptId(y))
               .IsRequired(true);

        builder.Property(x => x.VideoId)
               .HasConversion(x => x.Value, y => new VideoId(y))
               .IsRequired(true);

        builder.Property(x => x.Language)
               .HasMaxLength(FieldLengths.Language);

        // Relationships
        builder.HasOne(typeof(Video), "VideoId")
               .WithMany()
               .HasForeignKey("MaybeVideoId");

        builder.OwnsMany(x => x.Lines, (builder) =>
        {
            builder.WithOwner().HasForeignKey("TranscriptId");
            builder.Property("Id");
            builder.HasKey("Id");

            // No Dups the video
            builder.HasIndex(nameof(TranscriptLine.Text));
        });

        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}   