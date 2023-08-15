﻿// <auto-generated />
using System;
using Infrastructure.Data.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.SqlServer.Migrations
{
    [DbContext(typeof(SqlServerVideomaticDbContext))]
    partial class SqlServerVideomaticDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("ArtifactSequence");

            modelBuilder.HasSequence("PlaylistSequence");

            modelBuilder.HasSequence("TagsSequence");

            modelBuilder.HasSequence("TranscriptLineSequence");

            modelBuilder.HasSequence("TranscriptSequence");

            modelBuilder.HasSequence("VideoSequence");

            modelBuilder.Entity("Domain.Artifacts.Artifact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR ArtifactSequence");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("UpdatedOn")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("VideoId");

                    b.ToTable("Artifacts", "Videomatic");
                });

            modelBuilder.Entity("Domain.Playlists.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR PlaylistSequence");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsStarred")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Playlists", "Videomatic");
                });

            modelBuilder.Entity("Domain.Playlists.PlaylistVideo", b =>
                {
                    b.Property<int>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("PlaylistId", "VideoId");

                    b.HasIndex("VideoId");

                    b.ToTable("PlaylistVideos", "Videomatic");
                });

            modelBuilder.Entity("Domain.Transcripts.Transcript", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR TranscriptSequence");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Language");

                    b.HasIndex("VideoId");

                    b.ToTable("Transcripts", "Videomatic");
                });

            modelBuilder.Entity("Domain.Videos.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR VideoSequence");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsStarred")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Videos", "Videomatic");
                });

            modelBuilder.Entity("Domain.Artifacts.Artifact", b =>
                {
                    b.HasOne("Domain.Videos.Video", null)
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Playlists.Playlist", b =>
                {
                    b.OwnsOne("Domain.EntityOrigin", "Origin", b1 =>
                        {
                            b1.Property<int>("PlaylistId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("ChannelId")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("ChannelName")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("nvarchar(120)");

                            b1.Property<string>("DefaultLanguage")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ETag")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("EmbedHtml")
                                .HasMaxLength(2048)
                                .HasColumnType("nvarchar(2048)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("nvarchar(120)");

                            b1.Property<string>("ProviderId")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("ProviderItemId")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<DateTime?>("PublishedOn")
                                .HasColumnType("datetime2");

                            b1.HasKey("PlaylistId");

                            b1.HasIndex("ChannelId");

                            b1.HasIndex("ChannelName");

                            b1.HasIndex("DefaultLanguage");

                            b1.HasIndex("ETag");

                            b1.HasIndex("Name");

                            b1.HasIndex("ProviderId");

                            b1.HasIndex("ProviderItemId");

                            b1.ToTable("Playlists", "Videomatic");

                            b1.WithOwner()
                                .HasForeignKey("PlaylistId");

                            b1.OwnsOne("SharedKernel.ImageReference", "Picture", b2 =>
                                {
                                    b2.Property<int>("EntityOriginPlaylistId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<int>("Height")
                                        .HasColumnType("int");

                                    b2.Property<string>("Url")
                                        .IsRequired()
                                        .HasMaxLength(1024)
                                        .HasColumnType("nvarchar(1024)");

                                    b2.Property<int>("Width")
                                        .HasColumnType("int");

                                    b2.HasKey("EntityOriginPlaylistId");

                                    b2.ToTable("Playlists", "Videomatic");

                                    b2.WithOwner()
                                        .HasForeignKey("EntityOriginPlaylistId");
                                });

                            b1.OwnsOne("SharedKernel.ImageReference", "Thumbnail", b2 =>
                                {
                                    b2.Property<int>("EntityOriginPlaylistId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<int>("Height")
                                        .HasColumnType("int");

                                    b2.Property<string>("Url")
                                        .IsRequired()
                                        .HasMaxLength(1024)
                                        .HasColumnType("nvarchar(1024)");

                                    b2.Property<int>("Width")
                                        .HasColumnType("int");

                                    b2.HasKey("EntityOriginPlaylistId");

                                    b2.ToTable("Playlists", "Videomatic");

                                    b2.WithOwner()
                                        .HasForeignKey("EntityOriginPlaylistId");
                                });

                            b1.Navigation("Picture")
                                .IsRequired();

                            b1.Navigation("Thumbnail")
                                .IsRequired();
                        });

                    b.OwnsOne("SharedKernel.ImageReference", "Picture", b1 =>
                        {
                            b1.Property<int>("PlaylistId")
                                .HasColumnType("int");

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(1024)
                                .HasColumnType("nvarchar(1024)");

                            b1.Property<int>("Width")
                                .HasColumnType("int");

                            b1.HasKey("PlaylistId");

                            b1.ToTable("Playlists", "Videomatic");

                            b1.WithOwner()
                                .HasForeignKey("PlaylistId");
                        });

                    b.OwnsOne("SharedKernel.ImageReference", "Thumbnail", b1 =>
                        {
                            b1.Property<int>("PlaylistId")
                                .HasColumnType("int");

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(1024)
                                .HasColumnType("nvarchar(1024)");

                            b1.Property<int>("Width")
                                .HasColumnType("int");

                            b1.HasKey("PlaylistId");

                            b1.ToTable("Playlists", "Videomatic");

                            b1.WithOwner()
                                .HasForeignKey("PlaylistId");
                        });

                    b.Navigation("Origin")
                        .IsRequired();

                    b.Navigation("Picture");

                    b.Navigation("Thumbnail");
                });

            modelBuilder.Entity("Domain.Playlists.PlaylistVideo", b =>
                {
                    b.HasOne("Domain.Playlists.Playlist", null)
                        .WithMany("Videos")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Videos.Video", null)
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Transcripts.Transcript", b =>
                {
                    b.HasOne("Domain.Videos.Video", null)
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Domain.Transcripts.TranscriptLine", "Lines", b1 =>
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

                            b1.Property<int>("TranscriptId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("Text");

                            b1.HasIndex("TranscriptId");

                            b1.ToTable("TranscriptLines", "Videomatic");

                            b1.WithOwner()
                                .HasForeignKey("TranscriptId");
                        });

                    b.Navigation("Lines");
                });

            modelBuilder.Entity("Domain.Videos.Video", b =>
                {
                    b.OwnsOne("Domain.EntityOrigin", "Origin", b1 =>
                        {
                            b1.Property<int>("VideoId")
                                .HasColumnType("int");

                            b1.Property<string>("ChannelId")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("ChannelName")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("nvarchar(120)");

                            b1.Property<string>("DefaultLanguage")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ETag")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("EmbedHtml")
                                .HasMaxLength(2048)
                                .HasColumnType("nvarchar(2048)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("nvarchar(120)");

                            b1.Property<string>("ProviderId")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("ProviderItemId")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<DateTime?>("PublishedOn")
                                .HasColumnType("datetime2");

                            b1.HasKey("VideoId");

                            b1.HasIndex("ChannelId");

                            b1.HasIndex("ChannelName");

                            b1.HasIndex("DefaultLanguage");

                            b1.HasIndex("ETag");

                            b1.HasIndex("Name");

                            b1.HasIndex("ProviderId");

                            b1.HasIndex("ProviderItemId");

                            b1.ToTable("Videos", "Videomatic");

                            b1.WithOwner()
                                .HasForeignKey("VideoId");

                            b1.OwnsOne("SharedKernel.ImageReference", "Picture", b2 =>
                                {
                                    b2.Property<int>("EntityOriginVideoId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Height")
                                        .HasColumnType("int");

                                    b2.Property<string>("Url")
                                        .IsRequired()
                                        .HasMaxLength(1024)
                                        .HasColumnType("nvarchar(1024)");

                                    b2.Property<int>("Width")
                                        .HasColumnType("int");

                                    b2.HasKey("EntityOriginVideoId");

                                    b2.ToTable("Videos", "Videomatic");

                                    b2.WithOwner()
                                        .HasForeignKey("EntityOriginVideoId");
                                });

                            b1.OwnsOne("SharedKernel.ImageReference", "Thumbnail", b2 =>
                                {
                                    b2.Property<int>("EntityOriginVideoId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Height")
                                        .HasColumnType("int");

                                    b2.Property<string>("Url")
                                        .IsRequired()
                                        .HasMaxLength(1024)
                                        .HasColumnType("nvarchar(1024)");

                                    b2.Property<int>("Width")
                                        .HasColumnType("int");

                                    b2.HasKey("EntityOriginVideoId");

                                    b2.ToTable("Videos", "Videomatic");

                                    b2.WithOwner()
                                        .HasForeignKey("EntityOriginVideoId");
                                });

                            b1.Navigation("Picture")
                                .IsRequired();

                            b1.Navigation("Thumbnail")
                                .IsRequired();
                        });

                    b.OwnsOne("SharedKernel.ImageReference", "Picture", b1 =>
                        {
                            b1.Property<int>("VideoId")
                                .HasColumnType("int");

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(1024)
                                .HasColumnType("nvarchar(1024)");

                            b1.Property<int>("Width")
                                .HasColumnType("int");

                            b1.HasKey("VideoId");

                            b1.ToTable("Videos", "Videomatic");

                            b1.WithOwner()
                                .HasForeignKey("VideoId");
                        });

                    b.OwnsOne("SharedKernel.ImageReference", "Thumbnail", b1 =>
                        {
                            b1.Property<int>("VideoId")
                                .HasColumnType("int");

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(1024)
                                .HasColumnType("nvarchar(1024)");

                            b1.Property<int>("Width")
                                .HasColumnType("int");

                            b1.HasKey("VideoId");

                            b1.ToTable("Videos", "Videomatic");

                            b1.WithOwner()
                                .HasForeignKey("VideoId");
                        });

                    b.Navigation("Origin")
                        .IsRequired();

                    b.Navigation("Picture");

                    b.Navigation("Thumbnail");
                });

            modelBuilder.Entity("Domain.Playlists.Playlist", b =>
                {
                    b.Navigation("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}
