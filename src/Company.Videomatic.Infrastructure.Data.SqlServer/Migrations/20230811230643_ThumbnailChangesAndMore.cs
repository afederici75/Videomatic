using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class ThumbnailChangesAndMore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thumbnails");

            migrationBuilder.DropColumn(
                name: "Origin_PictureUrl",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Origin_ThumbnailUrl",
                table: "Playlists");

            migrationBuilder.DropSequence(
                name: "ThumbnailSequence");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "Videos",
                newName: "Thumbnail_Location");

            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "Videos",
                newName: "Picture_Location");

            migrationBuilder.AddColumn<int>(
                name: "Picture_Height",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Picture_Width",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Thumbnail_Height",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Thumbnail_Width",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Picture_Height",
                table: "Playlists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Picture_Location",
                table: "Playlists",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Picture_Width",
                table: "Playlists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Thumbnail_Height",
                table: "Playlists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail_Location",
                table: "Playlists",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Thumbnail_Width",
                table: "Playlists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture_Height",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Picture_Width",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Thumbnail_Height",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Thumbnail_Width",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Picture_Height",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Picture_Location",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Picture_Width",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Thumbnail_Height",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Thumbnail_Location",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Thumbnail_Width",
                table: "Playlists");

            migrationBuilder.RenameColumn(
                name: "Thumbnail_Location",
                table: "Videos",
                newName: "ThumbnailUrl");

            migrationBuilder.RenameColumn(
                name: "Picture_Location",
                table: "Videos",
                newName: "PictureUrl");

            migrationBuilder.CreateSequence(
                name: "ThumbnailSequence");

            migrationBuilder.AddColumn<string>(
                name: "Origin_PictureUrl",
                table: "Playlists",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin_ThumbnailUrl",
                table: "Playlists",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Thumbnails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR ThumbnailSequence"),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Resolution = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_VideoId",
                table: "Thumbnails",
                column: "VideoId");
        }
    }
}
