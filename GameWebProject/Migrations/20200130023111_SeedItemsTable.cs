using Microsoft.EntityFrameworkCore.Migrations;

namespace GameWebProject.Migrations
{
    public partial class SeedItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Condition", "LookingFor", "Platform", "ProductName" },
                values: new object[] { 1, 1, 0, "Fifa19", 3, "God of War III" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
