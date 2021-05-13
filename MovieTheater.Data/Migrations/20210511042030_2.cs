using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Data.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("0417c463-9af0-46d9-9ff7-d3e63321dfcc"),
                column: "ConcurrencyStamp",
                value: "e6c65eea-7ca8-4ad8-8066-f0e1385af508");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "d48d2694-0215-44ec-a7c9-c25db8b96aef");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "b935f161-6cb5-4547-9435-5a39d2e79c54");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2021, 5, 11, 4, 10, 18, 157, DateTimeKind.Utc).AddTicks(4856));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEJv+NBND9jZiemqUWmJqCbX+1jdrjFohVcggOLB8ys+eexn2cAnanBmkwa2Y7hcOJQ==");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bfdb878d-f543-4a69-b140-f05378ecb17c"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEKds491Lcsx/NO0YsZrISsL136cnPDXyiyQ42tELY4wZ5QnsGP6apJEvKXc/kVgadw==");
        }
    }
}
