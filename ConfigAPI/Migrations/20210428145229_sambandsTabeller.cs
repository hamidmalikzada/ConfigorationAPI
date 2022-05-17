using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigAPI.Migrations
{
    public partial class sambandsTabeller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Items_ItemId",
                table: "Components");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Configurations_ConfigurationId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ConfigurationId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Components_ItemId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ConfigurationId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Components");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfigurationId",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Components",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ConfigurationId",
                table: "Items",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ItemId",
                table: "Components",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Items_ItemId",
                table: "Components",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Configurations_ConfigurationId",
                table: "Items",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
