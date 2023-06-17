namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class TagConfigurationBase : IEntityTypeConfiguration<TagDb>
{
    public static class FieldLengths
    {
        public const int Name = 100;
    }

    public virtual void Configure(EntityTypeBuilder<TagDb> builder)
    {
        builder.ToTable("Tags");

        // Fields
        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Name);
        
        // Relationships
        builder.HasMany(x => x.Videos)
               .WithMany(x => x.Tags);
        
        // Indices
        builder.HasIndex(x => x.Id).IsUnique();
    }
}
