using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class ThumbnailConfiguration : EntityConfigurationBase<Thumbnail>
{
    public override void Configure(EntityTypeBuilder<Thumbnail> builder)
    {
        base.Configure(builder);

        // Fields        
        builder.Property(x => x.Url)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.Url);               

        // Indices
        builder.HasIndex(x => x.Resolution);
        builder.HasIndex(x => x.Url);
        builder.HasIndex(x => x.Height);
        builder.HasIndex(x => x.Width);
    }
}
