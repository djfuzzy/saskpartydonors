using Microsoft.EntityFrameworkCore.Migrations;

namespace SaskPartyDonors.Data.Migrations
{
    public partial class Add_Location_to_Contribution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Contributions");
        }
    }
}
