using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Data.Migrations
{
    public partial class remove_fk_seatrow_room : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatRows_Rooms_RoomId",
                table: "SeatRows");

            migrationBuilder.DropIndex(
                name: "IX_SeatRows_RoomId",
                table: "SeatRows");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "SeatRows");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "ffd615b2-db0c-447d-9fb4-c24fa818a46e");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "4b0d35f0-cf7d-406a-a0d7-90370bd102a7");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStart",
                value: new DateTime(2021, 4, 3, 11, 9, 18, 759, DateTimeKind.Utc).AddTicks(3126));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEOwWmCvNnh4O8ygLT1W5XoEC6uE2FV4zwlMHs0BoqHYaL/ZVf2tusmcCW0wVJJrvEw==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "SeatRows",
                type: "int",
                nullable: true);

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
    }
}
