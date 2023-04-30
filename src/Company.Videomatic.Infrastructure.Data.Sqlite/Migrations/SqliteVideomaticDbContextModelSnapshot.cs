﻿// <auto-generated />
using System;
using Company.Videomatic.Infrastructure.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Company.Videomatic.Infrastructure.Data.Sqlite.Migrations
{
    [DbContext(typeof(SqliteVideomaticDbContext))]
    partial class SqliteVideomaticDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Artifact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("VideoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("Artifact");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Folder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Thumbnail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Resolution")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("VideoId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Width")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("Thumbnails");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Transcript", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .HasColumnType("TEXT");

                    b.Property<int?>("VideoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("Transcripts");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.TranscriptLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan?>("StartsAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("TranscriptId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TranscriptId");

                    b.ToTable("TranscriptLine");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FolderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProviderId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderVideoId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FolderId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Artifact", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Model.Video", null)
                        .WithMany("Artifacts")
                        .HasForeignKey("VideoId");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Folder", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Model.Folder", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Thumbnail", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Model.Video", null)
                        .WithMany("Thumbnails")
                        .HasForeignKey("VideoId");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Transcript", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Model.Video", null)
                        .WithMany("Transcripts")
                        .HasForeignKey("VideoId");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.TranscriptLine", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Model.Transcript", null)
                        .WithMany("Lines")
                        .HasForeignKey("TranscriptId");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Video", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Model.Folder", null)
                        .WithMany("Videos")
                        .HasForeignKey("FolderId");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Folder", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Transcript", b =>
                {
                    b.Navigation("Lines");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Model.Video", b =>
                {
                    b.Navigation("Artifacts");

                    b.Navigation("Thumbnails");

                    b.Navigation("Transcripts");
                });
#pragma warning restore 612, 618
        }
    }
}