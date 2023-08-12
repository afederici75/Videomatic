using Company.Videomatic.Domain.Aggregates.Transcript;

namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TranscriptConfigurationBase : IEntityTypeConfiguration<Transcript>
{
    public const string TableName = "Transcripts";
    public const string TableNameForLines = "TranscriptLines";

    public static class FieldLengths
    {
        public const int Language = 10;
    }

    public virtual void Configure(EntityTypeBuilder<Transcript> builder)
    {
        builder.ToTable(TableName, VideomaticConstants.VideomaticSchema);

        // Fields
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => new TranscriptId(y))
               .IsRequired(true);

        builder.Property(x => x.VideoId)
               .HasConversion(x => x.Value, y => new VideoId(y))
               .IsRequired(true);

        builder.Property(x => x.Language)
               .HasMaxLength(FieldLengths.Language);        

        builder.OwnsMany(x => x.Lines, (builder) =>
        {
            builder.ToTable(TableNameForLines);

            // Shadow properties
            builder.WithOwner().HasForeignKey("TranscriptId");
            
            builder.Property("Id");
            builder.HasKey("Id");

            // Indices
            builder.HasIndex(nameof(TranscriptLine.Text));
        });

        // Indices
        builder.HasIndex(x => x.Language);
    }
}   