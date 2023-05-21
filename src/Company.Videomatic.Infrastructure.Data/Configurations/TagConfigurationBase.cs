namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TagConfigurationBase : IEntityTypeConfiguration<Tag>
{
    public virtual void Configure(EntityTypeBuilder<Tag> builder)
    {
        // Common
        //builder.ConfigureIEntity();

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
