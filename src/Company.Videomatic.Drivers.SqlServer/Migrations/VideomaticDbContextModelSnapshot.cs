﻿// <auto-generated />
using System;
using Company.Videomatic.Drivers.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Company.Videomatic.Drivers.SqlServer.Migrations
{
    [DbContext(typeof(VideomaticDbContext))]
    partial class VideomaticDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("IdSequence");

            modelBuilder.Entity("Company.Videomatic.Domain.Artifact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR IdSequence");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Title");

                    b.HasIndex("VideoId");

                    b.ToTable("Artifact");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Folder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR IdSequence");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Thumbnail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR IdSequence");

                    b.Property<int?>("Height")
                        .HasColumnType("int");

                    b.Property<int?>("Resolution")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.Property<int?>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Height");

                    b.HasIndex("Resolution");

                    b.HasIndex("Url");

                    b.HasIndex("VideoId");

                    b.HasIndex("Width");

                    b.ToTable("Thumbnails");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Transcript", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR IdSequence");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("Transcripts");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.TranscriptLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR IdSequence");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("StartsAt")
                        .HasColumnType("time");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TranscriptId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Text");

                    b.HasIndex("TranscriptId");

                    b.ToTable("TranscriptLine");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR IdSequence");

                    b.Property<string>("Description")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FolderId")
                        .HasColumnType("int");

                    b.Property<string>("ProviderId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.HasKey("Id");

                    b.HasIndex("FolderId");

                    b.HasIndex("ProviderId");

                    b.HasIndex("Title");

                    b.HasIndex("VideoUrl");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Artifact", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Video", null)
                        .WithMany("Artifacts")
                        .HasForeignKey("VideoId");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Folder", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Folder", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Thumbnail", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Video", null)
                        .WithMany("Thumbnails")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Transcript", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Video", null)
                        .WithMany("Transcripts")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Company.Videomatic.Domain.TranscriptLine", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Transcript", null)
                        .WithMany("Lines")
                        .HasForeignKey("TranscriptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Video", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Folder", null)
                        .WithMany("Videos")
                        .HasForeignKey("FolderId");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Folder", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Transcript", b =>
                {
                    b.Navigation("Lines");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Video", b =>
                {
                    b.Navigation("Artifacts");

                    b.Navigation("Thumbnails");

                    b.Navigation("Transcripts");
                });
#pragma warning restore 612, 618
        }
    }
}
