using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementWeb.Migrations
{
    public partial class AddManagerIDToEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerID",
                table: "Employees",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerID",
                table: "Employees");
        }
    }
}
