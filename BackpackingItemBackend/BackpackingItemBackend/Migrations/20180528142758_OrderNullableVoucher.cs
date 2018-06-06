using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BackpackingItemBackend.Migrations
{
    public partial class OrderNullableVoucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "VoucherId",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "VoucherId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
