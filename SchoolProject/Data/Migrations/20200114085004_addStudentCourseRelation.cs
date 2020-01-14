using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolProject.Data.Migrations
{
    public partial class addStudentCourseRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseRelation_Courses_CourseId",
                table: "StudentCourseRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseRelation_Students_StudentId",
                table: "StudentCourseRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourseRelation",
                table: "StudentCourseRelation");

            migrationBuilder.RenameTable(
                name: "StudentCourseRelation",
                newName: "GetStudentCourseRelations");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourseRelation_CourseId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "StudentCourseRelation");

            migrationBuilder.RenameIndex(
                name: "IX_GetStudentCourseRelations_CourseId",
                table: "StudentCourseRelation",
                newName: "IX_StudentCourseRelation_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourseRelation",
                table: "StudentCourseRelation",
                columns: new[] { "StudentId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseRelation_Courses_CourseId",
                table: "StudentCourseRelation",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseRelation_Students_StudentId",
                table: "StudentCourseRelation",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
