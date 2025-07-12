using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YurtApps.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueKeyStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Student_StudentEmail",
                table: "Student",
                column: "StudentEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_StudentPhoneNumber",
                table: "Student",
                column: "StudentPhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Student_StudentEmail",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_StudentPhoneNumber",
                table: "Student");
        }
    }
}
