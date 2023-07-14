﻿namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class PlaylistConfigurationBase : IEntityTypeConfiguration<Playlist>
{
    public static class FieldLengths
    {
        public const int Name = 120;
        //public const int Description = 4096;
    }

    public virtual void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable("Playlists");

        // Fields
        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => y);

        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Name);

        builder.Property(x => x.Description);
        //.HasMaxLength(FieldLengths.Description);

        // Relationships
        //builder.HasMany(x => x.Videos)
        //       .WithMany(x => x.Playlists)
        //       //.UsingEntity<PlaylistVideo>(
        //       //     l => l.HasOne<Video>(x => x.Video).WithMany(x => x.PlaylistVideos).HasForeignKey(x => x.VideoId),
        //       //     r => r.HasOne<Playlist>(x => x.Playlist).WithMany(x => x.PlaylistVideos).HasForeignKey(x => x.PlaylistId)
        //       // );
        //        .UsingEntity(typeof(PlaylistVideo),
        //            l => l.HasOne(typeof(Video), "Video").WithMany(nameof(Video.PlaylistVideos)).HasForeignKey("VideoId"),
        //            r => r.HasOne(typeof(Playlist), "Playlist").WithMany(nameof(Video.PlaylistVideos)).HasForeignKey("PlaylistId")
        //        );
        builder.HasMany(typeof(VideoPlaylist))
               .WithOne()
               .HasForeignKey("PlaylistId");

        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}