using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigAPI.Migrations
{
    public partial class spellCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionsConfigurations_Configurations_ConfigurationID",
                table: "CollectionsConfigurations");

            migrationBuilder.RenameColumn(
                name: "ConfigurationID",
                table: "CollectionsConfigurations",
                newName: "ConfigurationId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionsConfigurations_ConfigurationID",
                table: "CollectionsConfigurations",
                newName: "IX_CollectionsConfigurations_ConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionsConfigurations_Configurations_ConfigurationId",
                table: "CollectionsConfigurations",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionsConfigurations_Configurations_ConfigurationId",
                table: "CollectionsConfigurations");

            migrationBuilder.RenameColumn(
                name: "ConfigurationId",
                table: "CollectionsConfigurations",
                newName: "ConfigurationID");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionsConfigurations_ConfigurationId",
                table: "CollectionsConfigurations",
                newName: "IX_CollectionsConfigurations_ConfigurationID");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionsConfigurations_Configurations_ConfigurationID",
                table: "CollectionsConfigurations",
                column: "ConfigurationID",
                principalTable: "Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
