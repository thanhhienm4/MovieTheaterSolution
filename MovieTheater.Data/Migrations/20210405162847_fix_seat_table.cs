using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Data.Migrations
{
    public partial class fix_seat_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "8ca770a5-d01b-436a-912b-787ddc5253f5");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "c774f3df-ee2a-4508-a5b0-c12162bfd271");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStart",
                value: new DateTime(2021, 4, 5, 16, 28, 44, 188, DateTimeKind.Utc).AddTicks(9742));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEDNxTB+y6vEhUyTY4vEehFnb41s6x5q3b1MNP1mnLKwqZkFtfbbEOVfcAve/xL+sCQ==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "756900e1-690e-4851-bb6a-87f10ab8ac0e");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "d344cdbe-2e76-4fd1-ae0f-42c719963761");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStart",
                value: new DateTime(2021, 4, 5, 14, 50, 30, 477, DateTimeKind.Utc).AddTicks(8865));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEPE8Nub9oUH+5ePAiL/Yz1NW3q7Yqa1lKJTpSaHwB97HPFcLkOVNbdYMwi+os2j0BA==");
        }
    }
}
