using Company.Videomatic.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace Company.Videomatic.Drivers.SqlServer.Configurations;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {VideomaticDbContext.Constants.SequenceName}");        

        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasIndex(x => x.ProviderId);
        builder.HasIndex(x => x.VideoUrl);

        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.Description);

        builder.HasMany(x => x.Thumbnails)
               .WithOne()
               .HasForeignKey(x => x.VideoId)
               .IsRequired(true);

        builder.HasOne(x => x.Transcript)
               .WithOne()
               .IsRequired(true);
    }
}

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {VideomaticDbContext.Constants.SequenceName}");

        builder.HasOne<Folder>(x => x.Parent);

        builder.HasMany<Folder>(x => x.Children)
               .WithOne(x => x.Parent);

        builder.HasIndex(x => x.Id).IsUnique();
    }
}

public class ThumbnailConfiguration : IEntityTypeConfiguration<Thumbnail>
{
    public void Configure(EntityTypeBuilder<Thumbnail> builder)
    {
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {VideomaticDbContext.Constants.SequenceName}");

        
        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasIndex(x => x.VideoId);
        builder.HasIndex(x => x.Resolution);
        builder.HasIndex(x => x.Url);
        builder.HasIndex(x => x.Height);
        builder.HasIndex(x => x.Width);
    }
}

public class TranscriptLineConfiguration : IEntityTypeConfiguration<TranscriptLine>
{
    public void Configure(EntityTypeBuilder<TranscriptLine> builder)
    {
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {VideomaticDbContext.Constants.SequenceName}");
        
        builder.HasIndex(x => x.Id).IsUnique();

        builder.HasIndex(x => x.Text);
    }
}

 public class TranscriptConfiguration : IEntityTypeConfiguration<Transcript>
{
    public void Configure(EntityTypeBuilder<Transcript> builder)
    {
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {VideomaticDbContext.Constants.SequenceName}");

        //builder.HasMany(x => x.Lines);
               //.WithOne(nameof())
               //.OnDelete(DeleteBehavior.Cascade);   

        builder.HasIndex(x => x.Id).IsUnique();
    }
}   