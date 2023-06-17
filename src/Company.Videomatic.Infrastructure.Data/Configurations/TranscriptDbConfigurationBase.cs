namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TranscriptDbConfigurationBase : IEntityTypeConfiguration<TranscriptDb>
{
    public virtual void Configure(EntityTypeBuilder<TranscriptDb> builder)
    {
        builder.ToTable("Transcripts");        
        
        // Common
        builder.HasIndex(x => x.Id)
               .IsUnique();

        // Fields

        // Relationships
        builder.HasMany(x => x.Lines)
               .WithOne()            
               .HasForeignKey("TranscriptId")
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}   