using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementApp.API.Migrations
{
    public partial class parent_table_edited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Parents");
        }
    }
}
