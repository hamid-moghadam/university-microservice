using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Curriculum.API.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentCurriculums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Curriculum_Id = table.Column<int>(nullable: true),
                    Curriculum_Capacity = table.Column<int>(nullable: true),
                    Curriculum_ReservedCapacity = table.Column<int>(nullable: true),
                    Curriculum_StartTime = table.Column<string>(nullable: true),
                    Curriculum_EndTime = table.Column<string>(nullable: true),
                    Curriculum_RemainingCapacity = table.Column<int>(nullable: true),
                    Curriculum_IsCapacityCompleted = table.Column<bool>(nullable: true),
                    Curriculum_Course_Id = table.Column<int>(nullable: true),
                    Curriculum_Course_Title = table.Column<string>(nullable: true),
                    Curriculum_Course_Code = table.Column<string>(nullable: true),
                    Curriculum_Course_PracticalUnitCount = table.Column<int>(nullable: true),
                    Curriculum_Course_TheoryUnitCount = table.Column<int>(nullable: true),
                    Curriculum_Course_Type = table.Column<int>(nullable: true),
                    Curriculum_Field_Id = table.Column<int>(nullable: true),
                    Curriculum_Field_Title = table.Column<string>(nullable: true),
                    Curriculum_Field_DegreeType = table.Column<int>(nullable: true),
                    Curriculum_Teacher_Id = table.Column<int>(nullable: true),
                    Curriculum_Teacher_PersonnelId = table.Column<string>(nullable: true),
                    Curriculum_Teacher_UserId = table.Column<string>(nullable: true),
                    Curriculum_Teacher_FullName = table.Column<string>(nullable: true),
                    Curriculum_Semester_Id = table.Column<int>(nullable: true),
                    Curriculum_Semester_Year = table.Column<string>(nullable: true),
                    Curriculum_Semester_Type = table.Column<int>(nullable: true),
                    Curriculum_Semester_Title = table.Column<string>(nullable: true),
                    Student_Id = table.Column<int>(nullable: true),
                    Student_CreateTime = table.Column<DateTime>(nullable: true),
                    Student_Code = table.Column<string>(nullable: true),
                    Student_UserId = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusDescription = table.Column<string>(maxLength: 50, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCurriculums", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCurriculums");
        }
    }
}
