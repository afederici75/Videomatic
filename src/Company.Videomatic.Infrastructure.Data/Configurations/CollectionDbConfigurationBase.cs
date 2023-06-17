namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class CollectionDbConfigurationBase : IEntityTypeConfiguration<VideoCollectionDb>
{
    public virtual void Configure(EntityTypeBuilder<VideoCollectionDb> builder)
    {
        // Common
        builder.HasIndex(x => x.Id)
               .IsUnique();

        // Fields
        builder.Property(x => x.Name)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.CollectionName);
        
        // Relationships
        builder.HasMany(x => x.Videos)
               .WithMany(x => x.Collections);
        
        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}
