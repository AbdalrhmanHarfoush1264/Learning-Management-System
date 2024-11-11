using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsBetweenUserAndAnthorTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ForumPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Certificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_UserId",
                table: "Submissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumPosts_UserId",
                table: "ForumPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId",
                table: "Courses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_UserId",
                table: "Certificates",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_AspNetUsers_UserId",
                table: "Certificates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_UserId",
                table: "Enrollments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_AspNetUsers_UserId",
                table: "ForumPosts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_AspNetUsers_UserId",
                table: "Submissions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_AspNetUsers_UserId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_UserId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_AspNetUsers_UserId",
                table: "ForumPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_AspNetUsers_UserId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_UserId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_ForumPosts_UserId",
                table: "ForumPosts");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_UserId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ForumPosts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Certificates");
        }
    }
}
