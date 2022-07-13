using Microsoft.EntityFrameworkCore.Migrations;

namespace Emporium.POS.API.Migrations
{
    public partial class AddedThirdMirgration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "UserDetails",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserSecurityInfo",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UserAsUserName",
                table: "UserDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserSecurityInfo");

            migrationBuilder.DropColumn(
                name: "UserAsUserName",
                table: "UserDetails");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "UserDetails",
                newName: "UserID");
        }
    }
}
