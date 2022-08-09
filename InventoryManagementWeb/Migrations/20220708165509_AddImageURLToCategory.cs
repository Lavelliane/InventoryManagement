using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementWeb.Migrations
{
    public partial class AddImageURLToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imgURL",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imgURL",
                table: "Categories");
        }
    }
}
