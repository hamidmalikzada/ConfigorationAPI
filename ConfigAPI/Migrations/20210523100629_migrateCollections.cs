using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigAPI.Migrations
{
    public partial class migrateCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionsConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CollectionId = table.Column<int>(type: "INTEGER", nullable: false),
                    ConfigurationID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionsConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionsConfigurations_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionsConfigurations_Configurations_ConfigurationID",
                        column: x => x.ConfigurationID,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionsConfigurations_CollectionId",
                table: "CollectionsConfigurations",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionsConfigurations_ConfigurationID",
                table: "CollectionsConfigurations",
                column: "ConfigurationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionsConfigurations");

            migrationBuilder.DropTable(
                name: "Collections");
        }
    }
}
