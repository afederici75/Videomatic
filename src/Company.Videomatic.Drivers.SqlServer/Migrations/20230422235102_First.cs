using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Videomatic.Drivers.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "IdSequence");

            migrationBuilder.CreateTable(
                name: "Folder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR IdSequence"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folder_Folder_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Folder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR IdSequence"),
                    ProviderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FolderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_Folder_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Thumbnail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR IdSequence"),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    Resolution = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ETag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<long>(type: "bigint", nullable: true),
                    Width = table.Column<long>(type: "bigint", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR IdSequence"),
                    VideoId = table.Column<int>(type: "int", nullable: false)
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
                name: "TranscriptLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR IdSequence"),
                    TranscriptId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    StartsAt = table.Column<TimeSpan>(type: "time", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Folder_Id",
                table: "Folder",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Folder_ParentId",
                table: "Folder",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnail_Height",
                table: "Thumbnail",
                column: "Height");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnail_Id",
                table: "Thumbnail",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnail_Resolution",
                table: "Thumbnail",
                column: "Resolution");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnail_Url",
                table: "Thumbnail",
                column: "Url");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnail_VideoId",
                table: "Thumbnail",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnail_Width",
                table: "Thumbnail",
                column: "Width");

            migrationBuilder.CreateIndex(
                name: "IX_Transcript_Id",
                table: "Transcript",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transcript_VideoId",
                table: "Transcript",
                column: "VideoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLine_Id",
                table: "TranscriptLine",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLine_Text",
                table: "TranscriptLine",
                column: "Text");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptLine_TranscriptId",
                table: "TranscriptLine",
                column: "TranscriptId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_Description",
                table: "Video",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Video_FolderId",
                table: "Video",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_Id",
                table: "Video",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Video_ProviderId",
                table: "Video",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_Title",
                table: "Video",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Video_VideoUrl",
                table: "Video",
                column: "VideoUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thumbnail");

            migrationBuilder.DropTable(
                name: "TranscriptLine");

            migrationBuilder.DropTable(
                name: "Transcript");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Folder");

            migrationBuilder.DropSequence(
                name: "IdSequence");
        }
    }
}
