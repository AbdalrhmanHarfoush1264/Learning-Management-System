using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameColumnForForumPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VARCHAR",
                table: "ForumPosts",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "ForumPosts",
                newName: "VARCHAR");
        }
    }
}
