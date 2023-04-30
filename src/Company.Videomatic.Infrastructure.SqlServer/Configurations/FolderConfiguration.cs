using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class FolderConfiguration : EntityConfigurationBase<Folder>
{
    public override void Configure(EntityTypeBuilder<Folder> builder)
    {
        base.Configure(builder);

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
