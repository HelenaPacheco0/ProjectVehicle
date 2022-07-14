using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectVehicle.Migrations
{
    public partial class Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "VehicleMake",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "VehicleMake",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "VehicleMake");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "VehicleMake");
        }
    }
}
