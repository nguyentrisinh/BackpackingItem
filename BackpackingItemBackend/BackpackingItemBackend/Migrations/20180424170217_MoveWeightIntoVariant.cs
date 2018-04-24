using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BackpackingItemBackend.Migrations
{
    public partial class MoveWeightIntoVariant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Sizes");

            migrationBuilder.AddColumn<long>(
                name: "Weight",
                table: "Variants",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Variants");

            migrationBuilder.AddColumn<long>(
                name: "Weight",
                table: "Sizes",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
