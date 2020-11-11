using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Persistence.Migrations
{
    public partial class FixSemester : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Semesters_Year",
                table: "Semesters");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_Year_Type",
                table: "Semesters",
                columns: new[] { "Year", "Type" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Semesters_Year_Type",
                table: "Semesters");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_Year",
                table: "Semesters",
                column: "Year",
                unique: true);
        }
    }
}
