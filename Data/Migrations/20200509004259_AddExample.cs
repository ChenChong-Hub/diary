using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcDiary.Data.Migrations
{
    public partial class AddExample : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Characteristic",
                table: "Essentials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Example",
                table: "Essentials",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Characteristic",
                table: "Essentials");

            migrationBuilder.DropColumn(
                name: "Example",
                table: "Essentials");
        }
    }
}
