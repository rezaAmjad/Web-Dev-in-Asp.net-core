using Microsoft.EntityFrameworkCore.Migrations;

namespace GameWebProject.Migrations
{
    public partial class AddUserIDToItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "Items");
        }
    }
}
