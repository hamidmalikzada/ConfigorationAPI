using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigAPI.Migrations
{
    public partial class SambandsTabellFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsComponents_Components_ComponentId",
                table: "ItemsComponents");

            migrationBuilder.DropIndex(
                name: "IX_ItemsComponents_ComponentId",
                table: "ItemsComponents");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "ItemsComponents");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsComponents_Components_Id",
                table: "ItemsComponents",
                column: "Id",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsComponents_Components_Id",
                table: "ItemsComponents");

            migrationBuilder.AddColumn<int>(
                name: "ComponentId",
                table: "ItemsComponents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemsComponents_ComponentId",
                table: "ItemsComponents",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsComponents_Components_ComponentId",
                table: "ItemsComponents",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
