namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TranscriptLineConfigurationBase : IEntityTypeConfiguration<TranscriptLine>
{
    public virtual void Configure(EntityTypeBuilder<TranscriptLine> builder)
    {
        builder.ToTable("TranscriptLines");
        
        // Common
        //builder.HasOne(x => x.TranscriptId)
        //       .IsUnique();

        //Indices        
        builder.HasIndex(x => x.Text);
    }
}
