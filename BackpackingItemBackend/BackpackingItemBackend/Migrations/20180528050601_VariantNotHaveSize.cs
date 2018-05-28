using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BackpackingItemBackend.Migrations
{
    public partial class VariantNotHaveSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SizeId",
                table: "Variants",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SizeId",
                table: "Variants",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
