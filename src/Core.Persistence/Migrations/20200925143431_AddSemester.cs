using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Persistence.Migrations
{
    public partial class AddSemester : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Teachers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Students",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Curriculums",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_TeacherId",
                table: "Curriculums",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Curriculums_Teachers_TeacherId",
                table: "Curriculums",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curriculums_Teachers_TeacherId",
                table: "Curriculums");

            migrationBuilder.DropIndex(
                name: "IX_Curriculums_TeacherId",
                table: "Curriculums");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Curriculums");
        }
    }
}
