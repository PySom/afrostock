using Microsoft.EntityFrameworkCore.Migrations;

namespace AfrroStock.Migrations
{
    public partial class UpdateImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Orientation",
                table: "Images",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orientation",
                table: "Images");
        }
    }
}
