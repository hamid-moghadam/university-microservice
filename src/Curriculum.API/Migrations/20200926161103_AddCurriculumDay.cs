using Microsoft.EntityFrameworkCore.Migrations;

namespace Curriculum.API.Migrations
{
    public partial class AddCurriculumDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Curriculum_Day",
                table: "StudentCurriculums",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Curriculum_Day",
                table: "StudentCurriculums");
        }
    }
}
