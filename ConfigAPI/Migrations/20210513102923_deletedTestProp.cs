using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigAPI.Migrations
{
    public partial class deletedTestProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
