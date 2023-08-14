using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class ThumbnailAndPicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Thumbnail_Location",
                schema: "Videomatic",
                table: "Videos",
                newName: "Thumbnail_Url");

            migrationBuilder.RenameColumn(
                name: "Picture_Location",
                schema: "Videomatic",
                table: "Videos",
                newName: "Picture_Url");

            migrationBuilder.RenameColumn(
                name: "Thumbnail_Location",
                schema: "Videomatic",
                table: "Playlists",
                newName: "Thumbnail_Url");

            migrationBuilder.RenameColumn(
                name: "Picture_Location",
                schema: "Videomatic",
                table: "Playlists",
                newName: "Picture_Url");

            migrationBuilder.AddColumn<int>(
                name: "Origin_Thumbnail_Height",
                schema: "Videomatic",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Origin_Thumbnail_Url",
                schema: "Videomatic",
                table: "Videos",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Origin_Thumbnail_Width",
                schema: "Videomatic",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Origin_Thumbnail_Height",
                schema: "Videomatic",
                table: "Playlists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Origin_Thumbnail_Url",
                schema: "Videomatic",
                table: "Playlists",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Origin_Thumbnail_Width",
                schema: "Videomatic",
                table: "Playlists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Origin_Thumbnail_Height",
                schema: "Videomatic",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Origin_Thumbnail_Url",
                schema: "Videomatic",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Origin_Thumbnail_Width",
                schema: "Videomatic",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Origin_Thumbnail_Height",
                schema: "Videomatic",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_Thumbnail_Url",
                schema: "Videomatic",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_Thumbnail_Width",
                schema: "Videomatic",
                table: "Playlists");

            migrationBuilder.RenameColumn(
                name: "Thumbnail_Url",
                schema: "Videomatic",
                table: "Videos",
                newName: "Thumbnail_Location");

            migrationBuilder.RenameColumn(
                name: "Picture_Url",
                schema: "Videomatic",
                table: "Videos",
                newName: "Picture_Location");

            migrationBuilder.RenameColumn(
                name: "Thumbnail_Url",
                schema: "Videomatic",
                table: "Playlists",
                newName: "Thumbnail_Location");

            migrationBuilder.RenameColumn(
                name: "Picture_Url",
                schema: "Videomatic",
                table: "Playlists",
                newName: "Picture_Location");
        }
    }
}
