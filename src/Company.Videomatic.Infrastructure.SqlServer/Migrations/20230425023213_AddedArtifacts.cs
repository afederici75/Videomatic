using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Videomatic.Infrastructure.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedArtifacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artifact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR IdSequence"),
                    Title = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artifact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artifact_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artifact_Title",
                table: "Artifact",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Artifact_VideoId",
                table: "Artifact",
                column: "VideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artifact");
        }
    }
}
