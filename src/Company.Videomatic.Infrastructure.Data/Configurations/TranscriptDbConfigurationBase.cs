namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TranscriptDbConfigurationBase : IEntityTypeConfiguration<TranscriptDb>
{
    public static class FieldLengths
    {
        public const int Language = 2;
    }

    public virtual void Configure(EntityTypeBuilder<TranscriptDb> builder)
    {
        builder.ToTable("Transcripts");        
        
        // Fields
        builder.Property(x => x.Language)
               .HasMaxLength(FieldLengths.Language);

        // Relationships
        //builder.HasMany(x => x.Lines)
        //       .WithOne()               
        //       .HasForeignKey(x => x.TranscriptId)
        //       .IsRequired(true)
        //       .OnDelete(DeleteBehavior.Cascade);

        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}   