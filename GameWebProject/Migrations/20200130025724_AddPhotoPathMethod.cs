﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GameWebProject.Migrations
{
    public partial class AddPhotoPathMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Items");
        }
    }
}
