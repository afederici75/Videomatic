using Company.Videomatic.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        // Fields
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {DbConstants.SequenceName}");

        builder.Property(x => x.Name)
               .HasMaxLength(DbConstants.FieldLengths.FolderName);

        // Relationships
        builder.HasOne<Folder>(x => x.Parent);

        builder.HasMany<Folder>(x => x.Children)
               .WithOne(x => x.Parent);

        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}
