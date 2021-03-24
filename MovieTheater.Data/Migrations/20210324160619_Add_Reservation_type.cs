using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Data.Migrations
{
    public partial class Add_Reservation_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3bbb4d1b-da49-4f41-8f4e-baaab76e57fc"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("5b5138c7-6ab9-485d-919a-d0d49460133f"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ReservationTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("4ebea1ba-8899-4113-876c-92303aa1aac9"), "26688dc6-15e2-49e5-900a-1234d332c2ef", "Administrator role", "Admin", "Administrator" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("dba80de2-7f39-410c-bea1-036fb713a9ca"), 0, "", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mistake4@gmail.com", true, "Hien", "Nguyen Thanh", false, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Mistakem4@gmail.com", "admin", "AQAAAAEAACcQAAAAEL46Wla5k4uAP8B+rU09ulKmO4XYs6Vm8b1TjHmDn5cD3Gp+DTYEwQT5zzvqk/sooA==", "0912413908", true, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "ReservationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Đặt trực tiếp" });

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStart",
                value: new DateTime(2021, 3, 24, 16, 6, 16, 290, DateTimeKind.Utc).AddTicks(2365));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("4ebea1ba-8899-4113-876c-92303aa1aac9"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("dba80de2-7f39-410c-bea1-036fb713a9ca"));

            migrationBuilder.DeleteData(
                table: "ReservationTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "ReservationTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("3bbb4d1b-da49-4f41-8f4e-baaab76e57fc"), "d1ff40b4-7468-4d4b-921d-ca7a76b6d90c", "Administrator role", "Admin", "Administrator" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("5b5138c7-6ab9-485d-919a-d0d49460133f"), 0, "", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mistake4@gmail.com", true, "Hien", "Nguyen Thanh", false, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Mistakem4@gmail.com", "admin", "AQAAAAEAACcQAAAAEHImt5MAe7YP+B4+uiicdDKfJOR3GZTZyJksJGs6zPiFg9BXtP1nlkKuXR/3y+Gn+A==", "0912413908", true, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStart",
                value: new DateTime(2021, 3, 24, 15, 43, 19, 194, DateTimeKind.Utc).AddTicks(3592));
        }
    }
}
