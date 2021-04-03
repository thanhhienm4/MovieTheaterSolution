using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Data.Migrations
{
    public partial class Set_unique_for_nameSeatRow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SeatRows",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "SeatRows",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                columns: new[] { "RowId", "Number" });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "0b70d2e9-fdc0-454b-84a1-da35e28b9417");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "42b6915d-9c67-40e0-900c-fe456a56e7d6");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStart",
                value: new DateTime(2021, 4, 3, 7, 8, 3, 387, DateTimeKind.Utc).AddTicks(8485));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEH9cNTGjHeerDAFDk9SreFmSSHV2Xu2pCDnmQMC+XFyGjfZVxpFSsCJCFGE+ajoFGg==");

            migrationBuilder.CreateIndex(
                name: "IX_SeatRows_Name",
                table: "SeatRows",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeatRows_RoomId",
                table: "SeatRows",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatRows_Rooms_RoomId",
                table: "SeatRows",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatRows_Rooms_RoomId",
                table: "SeatRows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_SeatRows_Name",
                table: "SeatRows");

            migrationBuilder.DropIndex(
                name: "IX_SeatRows_RoomId",
                table: "SeatRows");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "SeatRows");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SeatRows",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                columns: new[] { "RowId", "Number", "RoomId" });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "9d934722-efc1-4cf1-8cbf-c61b2a43cb98");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "5fb83f5d-f8df-4c61-b144-bdb2cf6652b7");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStart",
                value: new DateTime(2021, 4, 2, 11, 27, 42, 632, DateTimeKind.Utc).AddTicks(2856));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEAaXEOrrZYVtmL7OFNvVUURTBM37AiX3JK7tUq1aRHDKquC4LI8DDWaEOQmJOI7Fog==");
        }
    }
}
