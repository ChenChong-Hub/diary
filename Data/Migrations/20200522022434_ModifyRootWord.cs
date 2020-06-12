using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcDiary.Data.Migrations
{
    public partial class ModifyRootWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pronunciation",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "ReciteCount",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Green",
                table: "Roots");

            migrationBuilder.AlterColumn<string>(
                name: "Meaning",
                table: "Words",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Meaning",
                table: "Roots",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Meaning",
                table: "Words",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Pronunciation",
                table: "Words",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReciteCount",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Meaning",
                table: "Roots",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "Green",
                table: "Roots",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
