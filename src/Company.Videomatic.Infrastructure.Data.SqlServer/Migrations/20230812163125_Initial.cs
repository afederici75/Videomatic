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
                name: "TranscriptLineSequence");

            migrationBuilder.CreateSequence(
                name: "TranscriptSequence");

            migrationBuilder.CreateSequence(
                name: "VideoSequence");

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR PlaylistSequence"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsStarred = table.Column<bool>(type: "bit", nullable: false),
                    Origin_Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Origin_ETag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Origin_ChannelId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Origin_Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Origin_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origin_PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Origin_EmbedHtml = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Origin_DefaultLanguage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Thumbnail_Location = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Thumbnail_Height = table.Column<int>(type: "int", nullable: false),
                    Thumbnail_Width = table.Column<int>(type: "int", nullable: false),
                    Picture_Location = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Picture_Height = table.Column<int>(type: "int", nullable: false),
                    Picture_Width = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR VideoSequence"),
                    Location = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsStarred = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details_Provider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details_ProviderVideoId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details_VideoPublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details_VideoOwnerChannelTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details_VideoOwnerChannelId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Thumbnail_Location = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Thumbnail_Height = table.Column<int>(type: "int", nullable: false),
                    Thumbnail_Width = table.Column<int>(type: "int", nullable: false),
                    Picture_Location = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Picture_Height = table.Column<int>(type: "int", nullable: false),
                    Picture_Width = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artifacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR ArtifactSequence"),
                    VideoId = table.Column<int>(type: "int", nullable: false),
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
                    PlaylistId = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: false)
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
                name: "Transcripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR TranscriptSequence"),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                    VideoId = table.Column<int>(type: "int", nullable: false)
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
                    TranscriptId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_Playlists_Origin_ChannelId",
                table: "Playlists",
                column: "Origin_ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Origin_DefaultLanguage",
                table: "Playlists",
                column: "Origin_DefaultLanguage");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Origin_ETag",
                table: "Playlists",
                column: "Origin_ETag");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Origin_Id",
                table: "Playlists",
                column: "Origin_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Origin_Name",
                table: "Playlists",
                column: "Origin_Name");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistVideos_VideoId",
                table: "PlaylistVideos",
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
                name: "TranscriptLineSequence");

            migrationBuilder.DropSequence(
                name: "TranscriptSequence");

            migrationBuilder.DropSequence(
                name: "VideoSequence");
        }
    }
}
