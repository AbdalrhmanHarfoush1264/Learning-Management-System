using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLengthForColumnsTo500Char : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Submissions",
                type: "VARCHAR(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Courses",
                type: "VARCHAR(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Courses",
                type: "VARCHAR(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "VARCHAR(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Assignments",
                type: "VARCHAR(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Assignments",
                type: "VARCHAR(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Submissions",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Courses",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Courses",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Assignments",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Assignments",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
