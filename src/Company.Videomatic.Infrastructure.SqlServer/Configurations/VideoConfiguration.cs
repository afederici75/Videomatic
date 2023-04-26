using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        // Fields
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {DbConstants.SequenceName}");

        builder.Property(x => x.ProviderId)
               .HasMaxLength(DbConstants.FieldLengths.ProviderId);  
        builder.Property(x => x.VideoUrl)
               .HasMaxLength(DbConstants.FieldLengths.Url); 
        builder.Property(x => x.Title)
               .HasMaxLength(DbConstants.FieldLengths.YTVideoTitle);
        builder.Property(x => x.Description)
               .HasMaxLength(DbConstants.FieldLengths.YTVideoDescription);


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