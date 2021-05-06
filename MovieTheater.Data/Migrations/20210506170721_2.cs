using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Data.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "ace8cdf2-ca74-46ec-8d32-6cd16e70c4ab");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "446ed63a-d1e8-44f5-9aa9-2b21f93d80cb");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStart",
                value: new DateTime(2021, 5, 6, 17, 7, 20, 204, DateTimeKind.Utc).AddTicks(679));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELPjCpNd6EXr997+J/0lvHfyCvWcXmBEcNh68ZH9l4gkss78zE+EbarYue7tK/37TA==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Reservations");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1081fba0-8368-43b7-8134-032e838c1bb3"),
                column: "ConcurrencyStamp",
                value: "acf671eb-91e7-4f3d-a63d-b6c43be5eb76");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c02ab224-ebdd-44e3-b691-5acec03da039"),
                column: "ConcurrencyStamp",
                value: "2482df42-7af0-4ab7-8522-c3bc0ecfa5fe");

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStart",
                value: new DateTime(2021, 5, 6, 16, 50, 48, 91, DateTimeKind.Utc).AddTicks(3849));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("99eca8ce-e954-4ed9-ab12-1a1fb010a9f8"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEIN4cPiQR8ByyCHP3Y0F5DufnOFJEuBwR/WnztJ+2R4prjlQQ2mY5wtNEoQ0dh9Zhw==");
        }
    }
}
