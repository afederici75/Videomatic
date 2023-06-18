namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TranscriptLineDbConfigurationBase : IEntityTypeConfiguration<TranscriptLineDb>
{
    public virtual void Configure(EntityTypeBuilder<TranscriptLineDb> builder)
    {
        builder.ToTable("TranscriptLines");
        
        // Common
        //builder.HasOne(x => x.TranscriptId)
        //       .IsUnique();

        //Indices        
        builder.HasIndex(x => x.Text);
    }
}
