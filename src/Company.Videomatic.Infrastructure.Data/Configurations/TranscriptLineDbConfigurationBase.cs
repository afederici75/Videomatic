namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TranscriptLineDbConfigurationBase : IEntityTypeConfiguration<TranscriptLineDb>
{
    public virtual void Configure(EntityTypeBuilder<TranscriptLineDb> builder)
    {
        builder.ToTable("TranscriptLines");
        
        // Common
        builder.HasIndex(x => x.Id)
               .IsUnique();

        //Indices        
        builder.HasIndex(x => x.Text);
    }
}
