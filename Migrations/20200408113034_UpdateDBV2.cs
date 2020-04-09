using Microsoft.EntityFrameworkCore.Migrations;

namespace AfrroStock.Migrations
{
    public partial class UpdateDBV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pic",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pic",
                table: "Images");
        }
    }
}
