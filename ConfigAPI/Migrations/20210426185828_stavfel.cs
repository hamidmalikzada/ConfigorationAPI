using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigAPI.Migrations
{
    public partial class stavfel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManufactrurerPartId",
                table: "Components",
                newName: "ManufacturerPartId");

            migrationBuilder.RenameColumn(
                name: "Manufactrurer",
                table: "Components",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Decsription",
                table: "Components",
                newName: "Manufacturer");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Components",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Components");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Components",
                newName: "Manufactrurer");

            migrationBuilder.RenameColumn(
                name: "ManufacturerPartId",
                table: "Components",
                newName: "ManufactrurerPartId");

            migrationBuilder.RenameColumn(
                name: "Manufacturer",
                table: "Components",
                newName: "Decsription");
        }
    }
}
