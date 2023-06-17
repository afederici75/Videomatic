namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TagConfigurationBase : IEntityTypeConfiguration<TagDb>
{
    public virtual void Configure(EntityTypeBuilder<TagDb> builder)
    {
        // Fields
        builder.Property(x => x.Id)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.TagId);
        
        // Relationships
        builder.HasMany(x => x.Videos)
               .WithMany(x => x.Tags);
        
        // Indices
        builder.HasIndex(x => x.Id).IsUnique();
    }
}
