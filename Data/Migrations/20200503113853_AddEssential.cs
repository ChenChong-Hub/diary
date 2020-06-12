using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcDiary.Data.Migrations
{
    public partial class AddEssential : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Words",
                table: "Words");

            migrationBuilder.RenameTable(
                name: "Words",
                newName: "Word");

            migrationBuilder.AddColumn<int>(
                name: "RightCount",
                table: "Word",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Word",
                table: "Word",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Word",
                table: "Word");

            migrationBuilder.DropColumn(
                name: "RightCount",
                table: "Word");

            migrationBuilder.RenameTable(
                name: "Word",
                newName: "Words");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Words",
                table: "Words",
                column: "Id");
        }
    }
}
