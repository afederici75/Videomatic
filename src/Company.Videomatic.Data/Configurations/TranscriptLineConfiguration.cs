namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TranscriptLineConfigurationBase : IEntityTypeConfiguration<TranscriptLine>
{
    public void Configure(EntityTypeBuilder<TranscriptLine> builder)
    {
        // Common
        builder.ConfigureIEntity();

        //Indices        
        builder.HasIndex(x => x.Text);
    }
}
