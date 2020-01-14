using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolProject.Data.Migrations
{
    public partial class addStudentCourseRelation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetStudentCourseRelations_Courses_CourseId",
                table: "GetStudentCourseRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_GetStudentCourseRelations_Students_StudentId",
                table: "GetStudentCourseRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetStudentCourseRelations",
                table: "GetStudentCourseRelations");

            migrationBuilder.RenameTable(
                name: "GetStudentCourseRelations",
                newName: "StudentCourseRelations");

            migrationBuilder.RenameIndex(
                name: "IX_GetStudentCourseRelations_CourseId",
                table: "StudentCourseRelations",
                newName: "IX_StudentCourseRelations_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourseRelations",
                table: "StudentCourseRelations",
                columns: new[] { "StudentId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseRelations_Courses_CourseId",
                table: "StudentCourseRelations",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseRelations_Students_StudentId",
                table: "StudentCourseRelations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseRelations_Courses_CourseId",
                table: "StudentCourseRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseRelations_Students_StudentId",
                table: "StudentCourseRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourseRelations",
                table: "StudentCourseRelations");

            migrationBuilder.RenameTable(
                name: "StudentCourseRelations",
                newName: "GetStudentCourseRelations");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourseRelations_CourseId",
                table: "GetStudentCourseRelations",
                newName: "IX_GetStudentCourseRelations_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetStudentCourseRelations",
                table: "GetStudentCourseRelations",
                columns: new[] { "StudentId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GetStudentCourseRelations_Courses_CourseId",
                table: "GetStudentCourseRelations",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GetStudentCourseRelations_Students_StudentId",
                table: "GetStudentCourseRelations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
