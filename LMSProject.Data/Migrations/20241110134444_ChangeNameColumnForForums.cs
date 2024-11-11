using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameColumnForForums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VARCHAR",
                table: "Forums",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Forums",
                newName: "VARCHAR");
        }
    }
}
