using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedTopicCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TopicCategories",
                schema: "Videomatic",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TopicCategories",
                schema: "Videomatic",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopicCategories",
                schema: "Videomatic",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "TopicCategories",
                schema: "Videomatic",
                table: "Playlists");
        }
    }
}
