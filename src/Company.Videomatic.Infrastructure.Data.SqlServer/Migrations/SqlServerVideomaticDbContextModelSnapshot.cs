﻿// <auto-generated />
using System;
using Company.Videomatic.Infrastructure.Data.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Migrations
{
    [DbContext(typeof(SqlServerVideomaticDbContext))]
    partial class SqlServerVideomaticDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("ArtifactSequence");

            modelBuilder.HasSequence("PlaylistSequence");

            modelBuilder.HasSequence("TagsSequence");

            modelBuilder.HasSequence("ThumbnailSequence");

            modelBuilder.HasSequence("TranscriptLineSequence");

            modelBuilder.HasSequence("TranscriptSequence");

            modelBuilder.HasSequence("VideoSequence");

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Artifact.Artifact", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR ArtifactSequence");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<long>("VideoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("VideoId");

                    b.ToTable("Artifacts", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Playlist.Playlist", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR PlaylistSequence");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("Playlists", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Transcript.Transcript", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR TranscriptSequence");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<long>("VideoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("Transcripts", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Video.Video", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR VideoSequence");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("Location");

                    b.HasIndex("Name");

                    b.ToTable("Videos", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Video.VideoPlaylist", b =>
                {
                    b.Property<long>("PlaylistId")
                        .HasColumnType("bigint");

                    b.Property<long>("VideoId")
                        .HasColumnType("bigint");

                    b.HasKey("PlaylistId", "VideoId");

                    b.HasIndex("VideoId");

                    b.ToTable("PlaylistVideos", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Artifact.Artifact", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Aggregates.Video.Video", null)
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Transcript.Transcript", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Aggregates.Video.Video", null)
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Company.Videomatic.Domain.Aggregates.Transcript.TranscriptLine", "Lines", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasDefaultValueSql("NEXT VALUE FOR TranscriptLineSequence");

                            b1.Property<TimeSpan?>("Duration")
                                .HasColumnType("time");

                            b1.Property<TimeSpan?>("StartsAt")
                                .HasColumnType("time");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.Property<long>("TranscriptId")
                                .HasColumnType("bigint");

                            b1.HasKey("Id");

                            b1.HasIndex("Text");

                            b1.HasIndex("TranscriptId");

                            b1.ToTable("TranscriptLine");

                            b1.WithOwner()
                                .HasForeignKey("TranscriptId");
                        });

                    b.Navigation("Lines");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Video.Video", b =>
                {
                    b.OwnsMany("Company.Videomatic.Domain.Aggregates.Video.Thumbnail", "Thumbnails", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasDefaultValueSql("NEXT VALUE FOR ThumbnailSequence");

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<string>("Location")
                                .IsRequired()
                                .HasMaxLength(1024)
                                .HasColumnType("nvarchar(1024)");

                            b1.Property<int>("Resolution")
                                .HasColumnType("int");

                            b1.Property<long>("VideoId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Width")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("VideoId");

                            b1.ToTable("Thumbnail");

                            b1.WithOwner()
                                .HasForeignKey("VideoId");
                        });

                    b.OwnsOne("Company.Videomatic.Domain.Aggregates.Video.VideoDetails", "Details", b1 =>
                        {
                            b1.Property<long>("VideoId")
                                .HasColumnType("bigint");

                            b1.Property<string>("ChannelId")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("PlaylistId")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<int>("Position")
                                .HasColumnType("int");

                            b1.Property<string>("Provider")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("VideoOwnerChannelId")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("VideoOwnerChannelTitle")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<DateTime>("VideoPublishedAt")
                                .HasColumnType("datetime2");

                            b1.HasKey("VideoId");

                            b1.HasIndex("ChannelId");

                            b1.HasIndex("VideoOwnerChannelId");

                            b1.ToTable("Videos");

                            b1.WithOwner()
                                .HasForeignKey("VideoId");
                        });

                    b.OwnsMany("Company.Videomatic.Domain.Aggregates.Video.VideoTag", "Tags", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasDefaultValueSql("NEXT VALUE FOR TagsSequence");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(35)
                                .HasColumnType("nvarchar(35)");

                            b1.Property<long>("VideoId")
                                .HasColumnType("bigint");

                            b1.HasKey("Id");

                            b1.HasIndex("VideoId");

                            b1.HasIndex("Name", "VideoId")
                                .IsUnique();

                            b1.ToTable("VideoTag");

                            b1.WithOwner()
                                .HasForeignKey("VideoId");
                        });

                    b.Navigation("Details")
                        .IsRequired();

                    b.Navigation("Tags");

                    b.Navigation("Thumbnails");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Video.VideoPlaylist", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Aggregates.Playlist.Playlist", null)
                        .WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Company.Videomatic.Domain.Aggregates.Video.Video", null)
                        .WithMany("Playlists")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Video.Video", b =>
                {
                    b.Navigation("Playlists");
                });
#pragma warning restore 612, 618
        }
    }
}
