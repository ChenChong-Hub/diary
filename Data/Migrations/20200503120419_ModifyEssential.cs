using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcDiary.Data.Migrations
{
    public partial class ModifyEssential : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Essentials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Pronunciation = table.Column<string>(nullable: true),
                    Meaning = table.Column<string>(maxLength: 60, nullable: false),
                    Translation = table.Column<string>(nullable: true),
                    ReciteCount = table.Column<int>(nullable: false),
                    RightCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Essentials", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Essentials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Words",
                table: "Words");

            migrationBuilder.RenameTable(
                name: "Words",
                newName: "Word");

            migrationBuilder.AddColumn<int>(
                name: "RightCount",
                table: "Word",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Word",
                table: "Word",
                column: "Id");
        }
    }
}
