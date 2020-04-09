using Microsoft.EntityFrameworkCore.Migrations;

namespace AfrroStock.Migrations
{
    public partial class UpdateDBV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Images",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
