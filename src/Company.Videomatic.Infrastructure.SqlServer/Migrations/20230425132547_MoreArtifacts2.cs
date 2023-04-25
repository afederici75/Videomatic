using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Videomatic.Infrastructure.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class MoreArtifacts2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artifact_Videos_VideoId",
                table: "Artifact");

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "Artifact",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Artifact_Videos_VideoId",
                table: "Artifact",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artifact_Videos_VideoId",
                table: "Artifact");

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "Artifact",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Artifact_Videos_VideoId",
                table: "Artifact",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id");
        }
    }
}
