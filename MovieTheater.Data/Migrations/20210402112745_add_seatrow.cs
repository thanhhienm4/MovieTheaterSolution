using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Data.Migrations
{
    public partial class add_seatrow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumns: new[] { "Number", "RoomId", "Row" },
                keyColumnTypes: new[] { "int", "int", "varchar(1)" },
                keyValues: new object[] { 1, 1, "A" });

            migrationBuilder.DropColumn(
                name: "Row",
                table: "Seats");

            migrationBuilder.AddColumn<int>(
                name: "RowId",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                columns: new[] { "RowId", "Number", "RoomId" });

            migrationBuilder.CreateTable(
                name: "SeatRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatRows", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "SeatRows",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "A" },
                    { 2, "B" },
                    { 3, "C" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEAaXEOrrZYVtmL7OFNvVUURTBM37AiX3JK7tUq1aRHDKquC4LI8DDWaEOQmJOI7Fog==");

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Number", "RoomId", "RowId", "Id", "KindOfSeatId" },
                values: new object[] { 1, 1, 1, 1, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatRows_RowId",
                table: "Seats",
                column: "RowId",
                principalTable: "SeatRows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatRows_RowId",
                table: "Seats");

            migrationBuilder.DropTable(
                name: "SeatRows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumns: new[] { "Number", "RoomId", "RowId" },
                keyColumnTypes: new[] { "int", "int", "int" },
                keyValues: new object[] { 1, 1, 1 });

            migrationBuilder.DropColumn(
                name: "RowId",
                table: "Seats");

            migrationBuilder.AddColumn<string>(
                name: "Row",
                table: "Seats",
                type: "varchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                columns: new[] { "Row", "Number", "RoomId" });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "54edf04d-17a3-4792-bfc3-0c3d4a1bec86");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "56e4b793-676c-4238-b40a-e464bcbdb92a");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStart",
                value: new DateTime(2021, 4, 2, 6, 59, 41, 274, DateTimeKind.Utc).AddTicks(1529));

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Number", "RoomId", "Row", "Id", "KindOfSeatId" },
                values: new object[] { 1, 1, "A", 1, 1 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEOuQF4JBUFeVfBDhZf6XA8oCFs1M5x7MRMGR1vAqzz1F7BLyiiSJxE2P0bFYqpalKg==");
        }
    }
}
