using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLengthForMessageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "VARCHAR(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldMaxLength: 1000);
        }
    }
}
