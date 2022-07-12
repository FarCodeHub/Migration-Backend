using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VisaExpairationDate",
                table: "Persons",
                newName: "VisaExpirationDate");

            migrationBuilder.RenameColumn(
                name: "CountryBrith",
                table: "Persons",
                newName: "Country");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VisaExpirationDate",
                table: "Persons",
                newName: "VisaExpairationDate");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Persons",
                newName: "CountryBrith");
        }
    }
}
