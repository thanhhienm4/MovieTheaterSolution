using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Data.Migrations
{
    public partial class Add_Film_poster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("2e015b35-8986-464d-8383-3bbdfdf8ebe6"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("b98d47c3-59f1-488a-bd3c-fb373bf4e62f"));

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Films",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("3bbb4d1b-da49-4f41-8f4e-baaab76e57fc"), "d1ff40b4-7468-4d4b-921d-ca7a76b6d90c", "Administrator role", "Admin", "Administrator" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("5b5138c7-6ab9-485d-919a-d0d49460133f"), 0, "", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mistake4@gmail.com", true, "Hien", "Nguyen Thanh", false, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Mistakem4@gmail.com", "admin", "AQAAAAEAACcQAAAAEHImt5MAe7YP+B4+uiicdDKfJOR3GZTZyJksJGs6zPiFg9BXtP1nlkKuXR/3y+Gn+A==", "0912413908", true, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "Screenings",
                columns: new[] { "Id", "FilmId", "KindOfScreeningId", "RoomId", "Surcharge", "TimeStart" },
                values: new object[] { 1, 1, 1, 1, 20000, new DateTime(2021, 3, 24, 15, 43, 19, 194, DateTimeKind.Utc).AddTicks(3592) });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "KindOfSeatId", "Number", "RoomId", "Row" },
                values: new object[] { 1, 1, 1, 1, "A" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3bbb4d1b-da49-4f41-8f4e-baaab76e57fc"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("5b5138c7-6ab9-485d-919a-d0d49460133f"));

            migrationBuilder.DeleteData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Films");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("2e015b35-8986-464d-8383-3bbdfdf8ebe6"), "da1a0920-75b0-4ae8-a629-091852408aac", "Administrator role", "Admin", "Administrator" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b98d47c3-59f1-488a-bd3c-fb373bf4e62f"), 0, "", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mistake4@gmail.com", true, "Hien", "Nguyen Thanh", false, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Mistakem4@gmail.com", "admin", "AQAAAAEAACcQAAAAEJbXzFt/AtZUcL6HTGKjVxowpoxsQ7782M9VCaTSVB1Uf+PYy0+oSAC/cktLEKaTrg==", "0912413908", true, "", false, "admin" });
        }
    }
}
