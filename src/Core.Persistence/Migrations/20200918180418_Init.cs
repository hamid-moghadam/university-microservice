using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Core.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 30, nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    PracticalUnitCount = table.Column<int>(nullable: false),
                    TheoryUnitCount = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Year = table.Column<string>(maxLength: 4, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ActivatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(maxLength: 32, nullable: false),
                    PersonnelId = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    DegreeType = table.Column<int>(nullable: false),
                    FieldGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fields_FieldGroups_FieldGroupId",
                        column: x => x.FieldGroupId,
                        principalTable: "FieldGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseFields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    FieldId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFields_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFields_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CurrentSemesterId = table.Column<int>(nullable: false),
                    AllowedSemesterRange_FromSemesterId = table.Column<int>(nullable: true),
                    AllowedSemesterRange_ToSemesterId = table.Column<int>(nullable: true),
                    AllowedSemesterRange_StartSemesterId = table.Column<int>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    FieldGroupId = table.Column<int>(nullable: false),
                    FieldId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurriculumSchedules_Semesters_CurrentSemesterId",
                        column: x => x.CurrentSemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculumSchedules_FieldGroups_FieldGroupId",
                        column: x => x.FieldGroupId,
                        principalTable: "FieldGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculumSchedules_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurriculumSchedules_Semesters_AllowedSemesterRange_FromSeme~",
                        column: x => x.AllowedSemesterRange_FromSemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculumSchedules_Semesters_AllowedSemesterRange_StartSem~",
                        column: x => x.AllowedSemesterRange_StartSemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    Code = table.Column<string>(maxLength: 30, nullable: false),
                    UserId = table.Column<string>(maxLength: 32, nullable: false),
                    FieldId = table.Column<int>(nullable: false),
                    SemesterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curriculums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    ReservedCapacity = table.Column<int>(nullable: false),
                    CourseFieldId = table.Column<int>(nullable: false),
                    SemesterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curriculums_CourseFields_CourseFieldId",
                        column: x => x.CourseFieldId,
                        principalTable: "CourseFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Curriculums_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseFields_CourseId",
                table: "CourseFields",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFields_FieldId",
                table: "CourseFields",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Code",
                table: "Courses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Title",
                table: "Courses",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_CourseFieldId",
                table: "Curriculums",
                column: "CourseFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_SemesterId",
                table: "Curriculums",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSchedules_CurrentSemesterId",
                table: "CurriculumSchedules",
                column: "CurrentSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSchedules_FieldGroupId",
                table: "CurriculumSchedules",
                column: "FieldGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSchedules_FieldId",
                table: "CurriculumSchedules",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSchedules_AllowedSemesterRange_FromSemesterId",
                table: "CurriculumSchedules",
                column: "AllowedSemesterRange_FromSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSchedules_AllowedSemesterRange_StartSemesterId",
                table: "CurriculumSchedules",
                column: "AllowedSemesterRange_StartSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_FieldGroupId",
                table: "Fields",
                column: "FieldGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_Title",
                table: "Fields",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_Year",
                table: "Semesters",
                column: "Year",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Code",
                table: "Students",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_FieldId",
                table: "Students",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SemesterId",
                table: "Students",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_PersonnelId",
                table: "Teachers",
                column: "PersonnelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserId",
                table: "Teachers",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Curriculums");

            migrationBuilder.DropTable(
                name: "CurriculumSchedules");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "CourseFields");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "FieldGroups");
        }
    }
}
