using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Videomatic");

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
                schema: "Videomatic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR PlaylistSequence"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStarred = table.Column<bool>(type: "bit", nullable: false),
                    Origin_ProviderId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Origin_ProviderItemId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Origin_ETag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Origin_ChannelId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Origin_ChannelName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Origin_Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Origin_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origin_PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Origin_EmbedHtml = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Origin_DefaultLanguage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Origin_Thumbnail_Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Origin_Thumbnail_Height = table.Column<int>(type: "int", nullable: false),
                    Origin_Thumbnail_Width = table.Column<int>(type: "int", nullable: false),
                    Origin_Picture_Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Origin_Picture_Height = table.Column<int>(type: "int", nullable: false),
                    Origin_Picture_Width = table.Column<int>(type: "int", nullable: false),
                    Thumbnail_Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Thumbnail_Height = table.Column<int>(type: "int", nullable: true),
                    Thumbnail_Width = table.Column<int>(type: "int", nullable: true),
                    Picture_Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Picture_Height = table.Column<int>(type: "int", nullable: true),
                    Picture_Width = table.Column<int>(type: "int", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                schema: "Videomatic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR VideoSequence"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStarred = table.Column<bool>(type: "bit", nullable: false),
                    Origin_ProviderId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Origin_ProviderItemId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Origin_ETag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Origin_ChannelId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Origin_ChannelName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Origin_Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Origin_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origin_PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Origin_EmbedHtml = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Origin_DefaultLanguage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Origin_Thumbnail_Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Origin_Thumbnail_Height = table.Column<int>(type: "int", nullable: false),
                    Origin_Thumbnail_Width = table.Column<int>(type: "int", nullable: false),
                    Origin_Picture_Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Origin_Picture_Height = table.Column<int>(type: "int", nullable: false),
                    Origin_Picture_Width = table.Column<int>(type: "int", nullable: false),
                    Thumbnail_Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Thumbnail_Height = table.Column<int>(type: "int", nullable: true),
                    Thumbnail_Width = table.Column<int>(type: "int", nullable: true),
                    Picture_Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Picture_Height = table.Column<int>(type: "int", nullable: true),
                    Picture_Width = table.Column<int>(type: "int", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artifacts",
                schema: "Videomatic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR ArtifactSequence"),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artifacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artifacts_Videos_VideoId",
                        column: x => x.VideoId,
                        principalSchema: "Videomatic",
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistVideos",
                schema: "Videomatic",
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
                        principalSchema: "Videomatic",
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistVideos_Videos_VideoId",
                        column: x => x.VideoId,
                        principalSchema: "Videomatic",
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transcripts",
                schema: "Videomatic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR TranscriptSequence"),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transcripts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transcripts_Videos_VideoId",
                        column: x => x.VideoId,
                        principalSchema: "Videomatic",
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranscriptLines",
                schema: "Videomatic",
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
                        principalSchema: "Videomatic",
                        principalTable: "Transcripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_CreatedOn",
                schema: "Videomatic",
                table: "Artifacts",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_Name",
                schema: "Videomatic",
                table: "Artifacts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_UpdatedOn",
                schema: "Videomatic",
                table: "Artifacts",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_VideoId",
                schema: "Videomatic",
                table: "Artifacts",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_CreatedOn",
                schema: "Videomatic",
                table: "Playlists",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Name",
                schema: "Videomatic",
                table: "Playlists",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Origin_ChannelId",
                schema: "Videomatic",
                table: "Playlists",
                column: "Origin_ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Origin_ChannelName",
                schema: "Videomatic",
                table: "Playlists",
                column: "Origin_ChannelName");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Origin_DefaultLanguage",
                schema: "Videomatic",
                table: "Playlists",
                column: "Origin_DefaultLanguage");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Origin_ETag",
                schema: "Videomatic",
                table: "Playlists",
                column: "Origin_ETag");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Origin_Name",
                schema: "Videomatic",
                table: "Playlists",
                column: "Origin_Name");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Origin_ProviderId",
                schema: "Videomatic",
                table: "Playlists",
                column: "Origin_ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Origin_ProviderItemId",
                schema: "Videomatic",
                table: "Playlists",
                column: "Origin_ProviderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UpdatedOn",
                schema: "Videomatic",
                table: "Playlists",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistVideos_VideoId",
                schema: "Videomatic",
                table: "PlaylistVideos",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLines_Text",
                schema: "Videomatic",
                table: "TranscriptLines",
                column: "Text");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLines_TranscriptId",
                schema: "Videomatic",
                table: "TranscriptLines",
                column: "TranscriptId");

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_Language",
                schema: "Videomatic",
                table: "Transcripts",
                column: "Language");

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_VideoId",
                schema: "Videomatic",
                table: "Transcripts",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_CreatedOn",
                schema: "Videomatic",
                table: "Videos",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Name",
                schema: "Videomatic",
                table: "Videos",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Origin_ChannelId",
                schema: "Videomatic",
                table: "Videos",
                column: "Origin_ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Origin_ChannelName",
                schema: "Videomatic",
                table: "Videos",
                column: "Origin_ChannelName");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Origin_DefaultLanguage",
                schema: "Videomatic",
                table: "Videos",
                column: "Origin_DefaultLanguage");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Origin_ETag",
                schema: "Videomatic",
                table: "Videos",
                column: "Origin_ETag");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Origin_Name",
                schema: "Videomatic",
                table: "Videos",
                column: "Origin_Name");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Origin_ProviderId",
                schema: "Videomatic",
                table: "Videos",
                column: "Origin_ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Origin_ProviderItemId",
                schema: "Videomatic",
                table: "Videos",
                column: "Origin_ProviderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_UpdatedOn",
                schema: "Videomatic",
                table: "Videos",
                column: "UpdatedOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artifacts",
                schema: "Videomatic");

            migrationBuilder.DropTable(
                name: "PlaylistVideos",
                schema: "Videomatic");

            migrationBuilder.DropTable(
                name: "TranscriptLines",
                schema: "Videomatic");

            migrationBuilder.DropTable(
                name: "Playlists",
                schema: "Videomatic");

            migrationBuilder.DropTable(
                name: "Transcripts",
                schema: "Videomatic");

            migrationBuilder.DropTable(
                name: "Videos",
                schema: "Videomatic");

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
