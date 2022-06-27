using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.Entity.Migrations
{
    public partial class teacherChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Lessons_LessonId",
                table: "Teachers");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Lessons_LessonId",
                table: "Teachers",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Lessons_LessonId",
                table: "Teachers");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "Teachers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Lessons_LessonId",
                table: "Teachers",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");
        }
    }
}
