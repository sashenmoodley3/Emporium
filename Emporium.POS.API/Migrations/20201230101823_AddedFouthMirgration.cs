using Microsoft.EntityFrameworkCore.Migrations;

namespace Emporium.POS.API.Migrations
{
    public partial class AddedFouthMirgration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SKUDetails",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    SKUName = table.Column<string>(nullable: true),
                    SKUPrice = table.Column<decimal>(nullable: false),
                    InventoryAmount = table.Column<int>(nullable: false),
                    SKUImage = table.Column<string>(nullable: true),
                    SKUMeasurementAmount = table.Column<string>(nullable: true),
                    KnownSKU = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKUDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SKUDetails");
        }
    }
}
