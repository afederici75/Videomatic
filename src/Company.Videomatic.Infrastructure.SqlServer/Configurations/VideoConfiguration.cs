using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class VideoConfiguration : EntityConfigurationBase<Video>
{
    public override void Configure(EntityTypeBuilder<Video> builder)
    {
        base.Configure(builder);    

        // Fields        
        builder.Property(x => x.ProviderId)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.ProviderId);
        builder.Property(x => x.ProviderVideoId)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.YTVideoId);
        builder.Property(x => x.VideoUrl)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.Url); 
        builder.Property(x => x.Title)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.YTVideoTitle);
        builder.Property(x => x.Description)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.YTVideoDescription);


        // Relationships
        builder.HasMany(x => x.Thumbnails)
               .WithOne()
               .IsRequired(true);
        
        builder.HasMany(x => x.Transcripts)
               .WithOne()
               .IsRequired(true);

        builder.HasMany(x => x.Artifacts)
               .WithOne()
               .IsRequired(true);

        // Indices
        builder.HasIndex(x => x.ProviderId);
        builder.HasIndex(x => x.VideoUrl);
        builder.HasIndex(x => x.Title);
        //builder.HasIndex(x => x.Description); // 5000 chars is too long for an index
    }
}