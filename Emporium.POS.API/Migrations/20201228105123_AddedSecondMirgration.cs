using Microsoft.EntityFrameworkCore.Migrations;

namespace Emporium.POS.API.Migrations
{
    public partial class AddedSecondMirgration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UserDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "UserDetails");
        }
    }
}
