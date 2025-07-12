using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YurtApps.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DormitoryCapacityRemovedOrWhateverYouChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DormitoryCapacity",
                table: "Dormitory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "DormitoryCapacity",
                table: "Dormitory",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
