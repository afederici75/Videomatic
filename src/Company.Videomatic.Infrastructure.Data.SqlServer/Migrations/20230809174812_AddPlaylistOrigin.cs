using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaylistOrigin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Origin_ChannelId",
                table: "Playlists",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin_DefaultLanguage",
                table: "Playlists",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin_Description",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin_ETag",
                table: "Playlists",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin_EmbedHtml",
                table: "Playlists",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin_Id",
                table: "Playlists",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin_Name",
                table: "Playlists",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin_PictureUrl",
                table: "Playlists",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Origin_PublishedAt",
                table: "Playlists",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin_ThumbnailUrl",
                table: "Playlists",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Playlists_Origin_ChannelId",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_Origin_DefaultLanguage",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_Origin_ETag",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_Origin_Id",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_Origin_Name",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_ChannelId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_DefaultLanguage",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_Description",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_ETag",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_EmbedHtml",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_Id",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_Name",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_PictureUrl",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_PublishedAt",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_ThumbnailUrl",
                table: "Playlists");
        }
    }
}
