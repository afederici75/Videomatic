namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TranscriptConfigurationBase : IEntityTypeConfiguration<Transcript>
{
    public virtual void Configure(EntityTypeBuilder<Transcript> builder)
    {
        // Common
        builder.ConfigureIEntity();

        // Fields

        // Relationships
        builder.HasMany(x => x.Lines)
               .WithOne()
               .IsRequired(true);

        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}   