using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Persistence.Migrations
{
    public partial class AddCurriculumDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Curriculums",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Curriculums");
        }
    }
}
