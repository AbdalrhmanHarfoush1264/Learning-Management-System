using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnVideoUrlToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Lessons");
        }
    }
}
