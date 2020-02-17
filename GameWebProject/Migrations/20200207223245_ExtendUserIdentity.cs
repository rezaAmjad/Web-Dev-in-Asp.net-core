using Microsoft.EntityFrameworkCore.Migrations;

namespace GameWebProject.Migrations
{
    public partial class ExtendUserIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "AspNetUsers",
                newName: "ZipCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "AspNetUsers",
                newName: "City");
        }
    }
}
