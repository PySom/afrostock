using Microsoft.EntityFrameworkCore.Migrations;

namespace AfrroStock.Migrations
{
    public partial class AddCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Sex",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CollectionId",
                table: "Categories",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Collections_CollectionId",
                table: "Categories",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Collections_CollectionId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CollectionId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte));
        }
    }
}
