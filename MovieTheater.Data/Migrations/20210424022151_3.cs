using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Data.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Screenings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "1e3b6f5d-4dab-441d-8d5d-4fbb0f39de2d");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "f7975cbb-22e4-47f5-b02e-77748bc34c54");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStart",
                value: new DateTime(2021, 4, 24, 2, 21, 47, 461, DateTimeKind.Utc).AddTicks(9173));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELVg5Gs9/bNpBeikWQAGxb4VSNwtTiXyhxvrq3zbDLAsc2wdbpBCdHAcnCX0LjFvzg==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Screenings");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "8f53f2df-bc15-4624-bfcf-f9f05de41260");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "10fce5c5-0c04-43c7-ad2a-a9e2a5aa1850");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStart",
                value: new DateTime(2021, 4, 20, 4, 13, 17, 984, DateTimeKind.Utc).AddTicks(4358));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEMRJpWeAvwtNwdkU8peeyXw1SEqCXaewzjVtLgTDBpGdDTcYIHKfjjowXxUUo3hCJg==");
        }
    }
}
