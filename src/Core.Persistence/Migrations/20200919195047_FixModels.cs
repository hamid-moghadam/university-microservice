using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Core.Persistence.Migrations
{
    public partial class FixModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curriculums_CourseFields_CourseFieldId",
                table: "Curriculums");

            migrationBuilder.DropForeignKey(
                name: "FK_CurriculumSchedules_Semesters_AllowedSemesterRange_FromSeme~",
                table: "CurriculumSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_CurriculumSchedules_Semesters_AllowedSemesterRange_StartSem~",
                table: "CurriculumSchedules");

            migrationBuilder.DropTable(
                name: "CourseFields");

            migrationBuilder.DropIndex(
                name: "IX_CurriculumSchedules_AllowedSemesterRange_StartSemesterId",
                table: "CurriculumSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Curriculums_CourseFieldId",
                table: "Curriculums");

            migrationBuilder.DropColumn(
                name: "AllowedSemesterRange_StartSemesterId",
                table: "CurriculumSchedules");

            migrationBuilder.DropColumn(
                name: "CourseFieldId",
                table: "Curriculums");

            migrationBuilder.RenameColumn(
                name: "AllowedSemesterRange_ToSemesterId",
                table: "CurriculumSchedules",
                newName: "ToSemesterId");

            migrationBuilder.RenameColumn(
                name: "AllowedSemesterRange_FromSemesterId",
                table: "CurriculumSchedules",
                newName: "FromSemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_CurriculumSchedules_AllowedSemesterRange_FromSemesterId",
                table: "CurriculumSchedules",
                newName: "IX_CurriculumSchedules_FromSemesterId");

            migrationBuilder.AlterColumn<int>(
                name: "ToSemesterId",
                table: "CurriculumSchedules",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FromSemesterId",
                table: "CurriculumSchedules",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Curriculums",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "Curriculums",
                fixedLength: true,
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FieldId",
                table: "Curriculums",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "Curriculums",
                fixedLength: true,
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSchedules_ToSemesterId",
                table: "CurriculumSchedules",
                column: "ToSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_CourseId",
                table: "Curriculums",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_FieldId",
                table: "Curriculums",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Curriculums_Courses_CourseId",
                table: "Curriculums",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Curriculums_Fields_FieldId",
                table: "Curriculums",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurriculumSchedules_Semesters_FromSemesterId",
                table: "CurriculumSchedules",
                column: "FromSemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurriculumSchedules_Semesters_ToSemesterId",
                table: "CurriculumSchedules",
                column: "ToSemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curriculums_Courses_CourseId",
                table: "Curriculums");

            migrationBuilder.DropForeignKey(
                name: "FK_Curriculums_Fields_FieldId",
                table: "Curriculums");

            migrationBuilder.DropForeignKey(
                name: "FK_CurriculumSchedules_Semesters_FromSemesterId",
                table: "CurriculumSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_CurriculumSchedules_Semesters_ToSemesterId",
                table: "CurriculumSchedules");

            migrationBuilder.DropIndex(
                name: "IX_CurriculumSchedules_ToSemesterId",
                table: "CurriculumSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Curriculums_CourseId",
                table: "Curriculums");

            migrationBuilder.DropIndex(
                name: "IX_Curriculums_FieldId",
                table: "Curriculums");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Curriculums");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Curriculums");

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "Curriculums");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Curriculums");

            migrationBuilder.RenameColumn(
                name: "ToSemesterId",
                table: "CurriculumSchedules",
                newName: "AllowedSemesterRange_ToSemesterId");

            migrationBuilder.RenameColumn(
                name: "FromSemesterId",
                table: "CurriculumSchedules",
                newName: "AllowedSemesterRange_FromSemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_CurriculumSchedules_FromSemesterId",
                table: "CurriculumSchedules",
                newName: "IX_CurriculumSchedules_AllowedSemesterRange_FromSemesterId");

            migrationBuilder.AlterColumn<int>(
                name: "AllowedSemesterRange_ToSemesterId",
                table: "CurriculumSchedules",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AllowedSemesterRange_FromSemesterId",
                table: "CurriculumSchedules",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AllowedSemesterRange_StartSemesterId",
                table: "CurriculumSchedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseFieldId",
                table: "Curriculums",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CourseFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FieldId = table.Column<int>(type: "integer", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSchedules_AllowedSemesterRange_StartSemesterId",
                table: "CurriculumSchedules",
                column: "AllowedSemesterRange_StartSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_CourseFieldId",
                table: "Curriculums",
                column: "CourseFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFields_CourseId",
                table: "CourseFields",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFields_FieldId",
                table: "CourseFields",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Curriculums_CourseFields_CourseFieldId",
                table: "Curriculums",
                column: "CourseFieldId",
                principalTable: "CourseFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurriculumSchedules_Semesters_AllowedSemesterRange_FromSeme~",
                table: "CurriculumSchedules",
                column: "AllowedSemesterRange_FromSemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurriculumSchedules_Semesters_AllowedSemesterRange_StartSem~",
                table: "CurriculumSchedules",
                column: "AllowedSemesterRange_StartSemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
