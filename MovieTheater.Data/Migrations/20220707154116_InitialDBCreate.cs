using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Data.Migrations
{
    public partial class InitialDBCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("0417c463-9af0-46d9-9ff7-d3e63321dfcc"),
                column: "ConcurrencyStamp",
                value: "56756757-2470-4d25-9f0c-7c1f92061645");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "a9eb7271-7095-4ede-b591-f5ca6fcf3997");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "fa59aabc-a3d5-482b-a2f3-d7d56f5791c0");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2022, 7, 7, 15, 41, 16, 155, DateTimeKind.Utc).AddTicks(1522));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEAJcS3PyNu9hkQXIhXxHUHIB8s/xh2gfY5FHMnwwUQyFYwHHjGHpFYZIcWN1II5YtQ==");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bfdb878d-f543-4a69-b140-f05378ecb17c"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEOQ1HEZYZHbwbbtSjhn5cJjFXDCLedSXEAXlPsaA9lthviSIWWGfmMYQDnro3N9F8A==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("0417c463-9af0-46d9-9ff7-d3e63321dfcc"),
                column: "ConcurrencyStamp",
                value: "43d208bb-168c-42a4-b541-aacac65f62b9");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "57036880-c10f-42f0-89b8-a578dff09b8b");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "449a3301-cb01-4fac-bd79-7e34eae3fa68");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2021, 5, 11, 4, 20, 29, 357, DateTimeKind.Utc).AddTicks(6376));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEIZEMwaN5+m3mANgr5frN1MXY+5eFm7HRPjPT7S2uH2cpL5r55e15oHxlnV/dwH9zA==");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bfdb878d-f543-4a69-b140-f05378ecb17c"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEGDwqVs3FH6kGdQt3CeiRBmxJH3L/3oXBibXCHArRf5exFILaaR3aQaomYxov7+brA==");
        }
    }
}
