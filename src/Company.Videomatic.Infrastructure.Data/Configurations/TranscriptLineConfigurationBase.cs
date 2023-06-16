namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TranscriptLineConfigurationBase : IEntityTypeConfiguration<TranscriptLine>
{
    public virtual void Configure(EntityTypeBuilder<TranscriptLine> builder)
    {        
        // Common
        builder.HasIndex(x => x.Id)
               .IsUnique();

        //Indices        
        builder.HasIndex(x => x.Text);
    }
}
