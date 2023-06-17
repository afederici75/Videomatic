using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "MainId");

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagDb",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoCollection",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artifact",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artifact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artifact_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagVideo",
                columns: table => new
                {
                    TagsId = table.Column<long>(type: "bigint", nullable: false),
                    VideosId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagVideo", x => new { x.TagsId, x.VideosId });
                    table.ForeignKey(
                        name: "FK_TagVideo_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagVideo_Video_VideosId",
                        column: x => x.VideosId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Thumbnail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resolution = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thumbnail_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transcript",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transcript", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transcript_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoVideoCollection",
                columns: table => new
                {
                    CollectionsId = table.Column<long>(type: "bigint", nullable: false),
                    VideosId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoVideoCollection", x => new { x.CollectionsId, x.VideosId });
                    table.ForeignKey(
                        name: "FK_VideoVideoCollection_VideoCollection_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "VideoCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoVideoCollection_Video_VideosId",
                        column: x => x.VideosId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtifactDb",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoDbId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtifactDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtifactDb_Videos_VideoDbId",
                        column: x => x.VideoDbId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TagDbVideoDb",
                columns: table => new
                {
                    TagsId = table.Column<long>(type: "bigint", nullable: false),
                    VideosId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDbVideoDb", x => new { x.TagsId, x.VideosId });
                    table.ForeignKey(
                        name: "FK_TagDbVideoDb_TagDb_TagsId",
                        column: x => x.TagsId,
                        principalTable: "TagDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagDbVideoDb_Videos_VideosId",
                        column: x => x.VideosId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThumbnailDb",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Resolution = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    VideoDbId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThumbnailDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThumbnailDb_Videos_VideoDbId",
                        column: x => x.VideoDbId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TranscriptDb",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoDbId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranscriptDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranscriptDb_Videos_VideoDbId",
                        column: x => x.VideoDbId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VideoCollectionDbVideoDb",
                columns: table => new
                {
                    CollectionsId = table.Column<long>(type: "bigint", nullable: false),
                    VideosId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoCollectionDbVideoDb", x => new { x.CollectionsId, x.VideosId });
                    table.ForeignKey(
                        name: "FK_VideoCollectionDbVideoDb_Collections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoCollectionDbVideoDb_Videos_VideosId",
                        column: x => x.VideosId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranscriptLine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartsAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    TranscriptId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranscriptLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranscriptLine_Transcript_TranscriptId",
                        column: x => x.TranscriptId,
                        principalTable: "Transcript",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranscriptLineDb",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartsAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    TranscriptDbId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranscriptLineDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranscriptLineDb_TranscriptDb_TranscriptDbId",
                        column: x => x.TranscriptDbId,
                        principalTable: "TranscriptDb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artifact_VideoId",
                table: "Artifact",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtifactDb_Id",
                table: "ArtifactDb",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArtifactDb_Title",
                table: "ArtifactDb",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_ArtifactDb_VideoDbId",
                table: "ArtifactDb",
                column: "VideoDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_Id",
                table: "Collections",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagDb_Id",
                table: "TagDb",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagDbVideoDb_VideosId",
                table: "TagDbVideoDb",
                column: "VideosId");

            migrationBuilder.CreateIndex(
                name: "IX_TagVideo_VideosId",
                table: "TagVideo",
                column: "VideosId");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnail_VideoId",
                table: "Thumbnail",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_ThumbnailDb_Height",
                table: "ThumbnailDb",
                column: "Height");

            migrationBuilder.CreateIndex(
                name: "IX_ThumbnailDb_Id",
                table: "ThumbnailDb",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThumbnailDb_Location",
                table: "ThumbnailDb",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_ThumbnailDb_Resolution",
                table: "ThumbnailDb",
                column: "Resolution");

            migrationBuilder.CreateIndex(
                name: "IX_ThumbnailDb_VideoDbId",
                table: "ThumbnailDb",
                column: "VideoDbId");

            migrationBuilder.CreateIndex(
                name: "IX_ThumbnailDb_Width",
                table: "ThumbnailDb",
                column: "Width");

            migrationBuilder.CreateIndex(
                name: "IX_Transcript_Id",
                table: "Transcript",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transcript_VideoId",
                table: "Transcript",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptDb_VideoDbId",
                table: "TranscriptDb",
                column: "VideoDbId");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLine_TranscriptId",
                table: "TranscriptLine",
                column: "TranscriptId");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLineDb_TranscriptDbId",
                table: "TranscriptLineDb",
                column: "TranscriptDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_Id",
                table: "Video",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Video_Location",
                table: "Video",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_Video_Title",
                table: "Video",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_VideoCollectionDbVideoDb_VideosId",
                table: "VideoCollectionDbVideoDb",
                column: "VideosId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoVideoCollection_VideosId",
                table: "VideoVideoCollection",
                column: "VideosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artifact");

            migrationBuilder.DropTable(
                name: "ArtifactDb");

            migrationBuilder.DropTable(
                name: "TagDbVideoDb");

            migrationBuilder.DropTable(
                name: "TagVideo");

            migrationBuilder.DropTable(
                name: "Thumbnail");

            migrationBuilder.DropTable(
                name: "ThumbnailDb");

            migrationBuilder.DropTable(
                name: "TranscriptLine");

            migrationBuilder.DropTable(
                name: "TranscriptLineDb");

            migrationBuilder.DropTable(
                name: "VideoCollectionDbVideoDb");

            migrationBuilder.DropTable(
                name: "VideoVideoCollection");

            migrationBuilder.DropTable(
                name: "TagDb");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Transcript");

            migrationBuilder.DropTable(
                name: "TranscriptDb");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "VideoCollection");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropSequence(
                name: "MainId");
        }
    }
}
