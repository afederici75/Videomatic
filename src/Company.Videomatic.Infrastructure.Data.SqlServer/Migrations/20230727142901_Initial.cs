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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                name: "Thumbnails",
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
                name: "VideoTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR TagsSequence"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VideoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoTags_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranscriptLines",
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
                    table.PrimaryKey("PK_TranscriptLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranscriptLines_Transcripts_TranscriptId",
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
                name: "IX_Thumbnails_VideoId",
                table: "Thumbnails",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLines_Text",
                table: "TranscriptLines",
                column: "Text");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLines_TranscriptId",
                table: "TranscriptLines",
                column: "TranscriptId");

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_Language",
                table: "Transcripts",
                column: "Language");

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_VideoId",
                table: "Transcripts",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Details_Provider",
                table: "Videos",
                column: "Details_Provider");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Details_ProviderVideoId",
                table: "Videos",
                column: "Details_ProviderVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Details_VideoOwnerChannelId",
                table: "Videos",
                column: "Details_VideoOwnerChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Details_VideoOwnerChannelTitle",
                table: "Videos",
                column: "Details_VideoOwnerChannelTitle");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Details_VideoPublishedAt",
                table: "Videos",
                column: "Details_VideoPublishedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Location",
                table: "Videos",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Name",
                table: "Videos",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_VideoTags_Name_VideoId",
                table: "VideoTags",
                columns: new[] { "Name", "VideoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoTags_VideoId",
                table: "VideoTags",
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
                name: "Thumbnails");

            migrationBuilder.DropTable(
                name: "TranscriptLines");

            migrationBuilder.DropTable(
                name: "VideoTags");

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
