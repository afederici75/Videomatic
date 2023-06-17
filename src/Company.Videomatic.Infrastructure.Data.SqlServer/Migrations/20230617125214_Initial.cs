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
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR MainId"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", maxLength: 20, nullable: false, defaultValueSql: "NEXT VALUE FOR MainId"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR MainId"),
                    Location = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artifacts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR MainId"),
                    Title = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artifacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artifacts_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatlistsAndVideos",
                columns: table => new
                {
                    PlaylistsId = table.Column<long>(type: "bigint", nullable: false),
                    VideosId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatlistsAndVideos", x => new { x.PlaylistsId, x.VideosId });
                    table.ForeignKey(
                        name: "FK_PlatlistsAndVideos_Playlists_PlaylistsId",
                        column: x => x.PlaylistsId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatlistsAndVideos_Videos_VideosId",
                        column: x => x.VideosId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagsAndVideos",
                columns: table => new
                {
                    TagsId = table.Column<long>(type: "bigint", nullable: false),
                    VideosId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsAndVideos", x => new { x.TagsId, x.VideosId });
                    table.ForeignKey(
                        name: "FK_TagsAndVideos_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagsAndVideos_Videos_VideosId",
                        column: x => x.VideosId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Thumbnails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR MainId"),
                    Location = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Resolution = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thumbnails_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transcripts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR MainId"),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranscriptId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transcripts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transcripts_Videos_TranscriptId",
                        column: x => x.TranscriptId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranscriptLines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR MainId"),
                    Text = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartsAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    TranscriptId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranscriptLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranscriptLines_Transcripts_TranscriptId",
                        column: x => x.TranscriptId,
                        principalTable: "Transcripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_Id",
                table: "Artifacts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_Title",
                table: "Artifacts",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_VideoId",
                table: "Artifacts",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatlistsAndVideos_VideosId",
                table: "PlatlistsAndVideos",
                column: "VideosId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Id",
                table: "Playlists",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Id",
                table: "Tags",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagsAndVideos_VideosId",
                table: "TagsAndVideos",
                column: "VideosId");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_Height",
                table: "Thumbnails",
                column: "Height");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_Id",
                table: "Thumbnails",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_Location",
                table: "Thumbnails",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_Resolution",
                table: "Thumbnails",
                column: "Resolution");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_VideoId",
                table: "Thumbnails",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_Width",
                table: "Thumbnails",
                column: "Width");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLines_Id",
                table: "TranscriptLines",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLines_Text",
                table: "TranscriptLines",
                column: "Text");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLines_TranscriptId",
                table: "TranscriptLines",
                column: "TranscriptId");

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_Id",
                table: "Transcripts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_TranscriptId",
                table: "Transcripts",
                column: "TranscriptId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Id",
                table: "Videos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Location",
                table: "Videos",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Title",
                table: "Videos",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artifacts");

            migrationBuilder.DropTable(
                name: "PlatlistsAndVideos");

            migrationBuilder.DropTable(
                name: "TagsAndVideos");

            migrationBuilder.DropTable(
                name: "Thumbnails");

            migrationBuilder.DropTable(
                name: "TranscriptLines");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Transcripts");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropSequence(
                name: "MainId");
        }
    }
}
