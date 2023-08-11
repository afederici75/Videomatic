namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class PlaylistConfigurationBase : IEntityTypeConfiguration<Playlist>
{
    public const string TableName = "Playlists";

    public static class FieldLengths
    {
        public const int Name = 120;
    }

    public static class OriginLengths
    { 
        public const int Id = 50;
        public const int ETag = 50;
        public const int ChannelId = 50;
        public const int Name = FieldLengths.Name;
        public const int Description = 5000;

        public const int ThumbnailUrl = 1024;
        public const int PictureUrl = 1024;
        public const int EmbedHtml = 2048;
        public const int DefaultLanguage = 10;
    }

    public virtual void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable(TableName);

        // Fields
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Name);

        builder.Property(x => x.Description); // MAX
        
        // Relationships
        builder.HasMany(x => x.Videos)
               .WithOne()
               .HasForeignKey(nameof(PlaylistVideo.PlaylistId));
       
        builder.OwnsOne(x => x.Origin, (bld) =>
        {
            bld.Property(x => x.Id).HasMaxLength(OriginLengths.Id);
            bld.Property(x => x.ETag).HasMaxLength(OriginLengths.ETag);
            bld.Property(x => x.ChannelId).HasMaxLength(OriginLengths.ChannelId);
            bld.Property(x => x.Name).HasMaxLength(OriginLengths.Name);
            bld.Property(x => x.Description);// MAX
            bld.Property(x => x.PublishedAt);
            //bld.Property(x => x.ThumbnailUrl).HasMaxLength(OriginLengths.ThumbnailUrl);
            //bld.Property(x => x.PictureUrl).HasMaxLength(OriginLengths.PictureUrl);
            bld.OwnsOne(x => x.Thumbnail, b => 
            { 
                b.Property(x => x.Location).HasMaxLength(OriginLengths.ThumbnailUrl);
            });

            bld.OwnsOne(x => x.Picture, b =>
            {
                b.Property(x => x.Location).HasMaxLength(OriginLengths.ThumbnailUrl);
            });


            bld.Property(x => x.EmbedHtml).HasMaxLength(OriginLengths.EmbedHtml);
            bld.Property(x => x.DefaultLanguage).HasMaxLength(OriginLengths.DefaultLanguage);

            bld.HasIndex(x => x.Id);
            bld.HasIndex(x => x.ETag);
            bld.HasIndex(x => x.ChannelId);
            bld.HasIndex(x => x.Name);
            bld.HasIndex(x => x.DefaultLanguage);
        });

        // Indices
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Description);
    }
}
