namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class FolderConfigurationBase : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        // Common
        builder.ConfigureIEntity();

        // Fields
        builder.Property(x => x.Name)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.FolderName);

        // Relationships
        builder.HasOne<Folder>(x => x.Parent);

        builder.HasMany<Folder>(x => x.Children)
               .WithOne(x => x.Parent);

        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}
