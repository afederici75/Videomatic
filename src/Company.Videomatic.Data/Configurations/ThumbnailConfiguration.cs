﻿namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class ThumbnailConfigurationBase : IEntityTypeConfiguration<Thumbnail>
{
    public void Configure(EntityTypeBuilder<Thumbnail> builder)
    {
        // Common
        builder.ConfigureIEntity();

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
