using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigAPI.Migrations
{
    public partial class togBortCollectionsParents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentItem");

            migrationBuilder.DropTable(
                name: "ConfigurationItem");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ComponentItem",
                columns: table => new
                {
                    ComponentsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentItem", x => new { x.ComponentsId, x.ItemsId });
                    table.ForeignKey(
                        name: "FK_ComponentItem_Components_ComponentsId",
                        column: x => x.ComponentsId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentItem_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItem",
                columns: table => new
                {
                    ConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItem", x => new { x.ConfigurationId, x.ItemsId });
                    table.ForeignKey(
                        name: "FK_ConfigurationItem_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationItem_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentItem_ItemsId",
                table: "ComponentItem",
                column: "ItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_ItemsId",
                table: "ConfigurationItem",
                column: "ItemsId");
        }
    }
}
