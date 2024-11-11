using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnGradeIdFromSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Submissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
