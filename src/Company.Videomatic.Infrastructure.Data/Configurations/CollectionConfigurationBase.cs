namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class CollectionConfigurationBase : IEntityTypeConfiguration<Collection>
{
    public virtual void Configure(EntityTypeBuilder<Collection> builder)
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
