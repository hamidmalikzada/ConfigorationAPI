using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigAPI.Migrations
{
    public partial class newSambandsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsComponents_Components_Id",
                table: "ItemsComponents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_ItemsComponents_Components_Id",
                table: "ItemsComponents",
                column: "Id",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
