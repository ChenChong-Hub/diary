using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcDiary.Data.Migrations
{
    public partial class Green : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Green",
                table: "Roots",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Green",
                table: "Roots");
        }
    }
}
