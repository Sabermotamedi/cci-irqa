using Microsoft.EntityFrameworkCore.Migrations;

namespace Presentation.Migrations
{
    public partial class BusinesTypeToOrganizationID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Business_Type",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Organization_Id",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Organization_Id",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Business_Type",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
