using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementWeb.Migrations
{
    public partial class AddLevelColumnToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Employees");
        }
    }
}
