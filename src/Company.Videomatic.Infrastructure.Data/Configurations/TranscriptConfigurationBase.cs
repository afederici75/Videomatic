namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TranscriptConfigurationBase : IEntityTypeConfiguration<Transcript>
{
    public virtual void Configure(EntityTypeBuilder<Transcript> builder)
    {
        // Common
        builder.HasIndex(x => x.Id)
               .IsUnique();

        // Fields

        // Relationships
        builder.HasMany(x => x.Lines)
               .WithOne()
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}   