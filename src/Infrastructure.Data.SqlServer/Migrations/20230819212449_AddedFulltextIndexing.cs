using Infrastructure.Data.SqlServer.FullTextSearch;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedFulltextIndexing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            FullTextIndexingMigrationHelper.Up(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
