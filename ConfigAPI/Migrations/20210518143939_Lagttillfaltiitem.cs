using Microsoft.EntityFrameworkCore.Migrations;



namespace ConfigAPI.Migrations
{
    public partial class Lagttillfaltiitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Items");
        }
    }
}
