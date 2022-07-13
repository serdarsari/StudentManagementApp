using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementApp.API.Migrations
{
    public partial class manager_table_edited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Managers");
        }
    }
}
