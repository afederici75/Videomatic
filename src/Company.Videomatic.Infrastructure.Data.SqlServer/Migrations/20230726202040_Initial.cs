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
                name: "ArtifactSequence");

            migrationBuilder.CreateSequence(
                name: "PlaylistSequence");

            migrationBuilder.CreateSequence(
                name: "TagsSequence");

            migrationBuilder.CreateSequence(
                name: "ThumbnailSequence");

            migrationBuilder.CreateSequence(
                name: "TranscriptLineSequence");

            migrationBuilder.CreateSequence(
                name: "TranscriptSequence");

            migrationBuilder.CreateSequence(
                name: "VideoSequence");

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR PlaylistSequence"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR VideoSequence"),
                    Location = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details_Provider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details_ProviderVideoId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details_VideoPublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details_VideoOwnerChannelTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details_VideoOwnerChannelId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artifacts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR ArtifactSequence"),
                    VideoId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "PlaylistVideos",
                columns: table => new
                {
                    PlaylistId = table.Column<long>(type: "bigint", nullable: false),
                    VideoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistVideos", x => new { x.PlaylistId, x.VideoId });
                    table.ForeignKey(
                        name: "FK_PlaylistVideos_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistVideos_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Thumbnail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR ThumbnailSequence"),
                    Location = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Resolution = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thumbnail_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transcripts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR TranscriptSequence"),
                    VideoId = table.Column<long>(type: "bigint", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transcripts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transcripts_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR TagsSequence"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VideoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoTag_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranscriptLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR TranscriptLineSequence"),
                    Text = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    StartsAt = table.Column<TimeSpan>(type: "time", nullable: true),
                    TranscriptId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranscriptLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranscriptLine_Transcripts_TranscriptId",
                        column: x => x.TranscriptId,
                        principalTable: "Transcripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_Name",
                table: "Artifacts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_VideoId",
                table: "Artifacts",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Description",
                table: "Playlists",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Name",
                table: "Playlists",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistVideos_VideoId",
                table: "PlaylistVideos",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnail_VideoId",
                table: "Thumbnail",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLine_Text",
                table: "TranscriptLine",
                column: "Text");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLine_TranscriptId",
                table: "TranscriptLine",
                column: "TranscriptId");

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_VideoId",
                table: "Transcripts",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Details_VideoOwnerChannelId",
                table: "Videos",
                column: "Details_VideoOwnerChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Location",
                table: "Videos",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Name",
                table: "Videos",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_VideoTag_Name_VideoId",
                table: "VideoTag",
                columns: new[] { "Name", "VideoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoTag_VideoId",
                table: "VideoTag",
                column: "VideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artifacts");

            migrationBuilder.DropTable(
                name: "PlaylistVideos");

            migrationBuilder.DropTable(
                name: "Thumbnail");

            migrationBuilder.DropTable(
                name: "TranscriptLine");

            migrationBuilder.DropTable(
                name: "VideoTag");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Transcripts");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropSequence(
                name: "ArtifactSequence");

            migrationBuilder.DropSequence(
                name: "PlaylistSequence");

            migrationBuilder.DropSequence(
                name: "TagsSequence");

            migrationBuilder.DropSequence(
                name: "ThumbnailSequence");

            migrationBuilder.DropSequence(
                name: "TranscriptLineSequence");

            migrationBuilder.DropSequence(
                name: "TranscriptSequence");

            migrationBuilder.DropSequence(
                name: "VideoSequence");
        }
    }
}
