using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Data.Migrations
{
    public partial class Chaange_Name_RoomFormat1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Formats_FormatId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Formats",
                table: "Formats");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("4883c8da-f8f9-447c-b762-09fe25b51291"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("4d7ac40b-9f15-4517-b8d5-25ac2e1113d0"));

            migrationBuilder.RenameTable(
                name: "Formats",
                newName: "RoomFormats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomFormats",
                table: "RoomFormats",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("f357660a-9a84-4acb-8386-ff8cb211d746"), "625afc3d-e2a3-42d8-89b3-120bc014402b", "Administrator role", "Admin", "Administrator" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("03aeb1ff-1fe6-4fb0-b005-8c831db8dfe3"), 0, "", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mistake4@gmail.com", true, "Hien", "Nguyen Thanh", false, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Mistakem4@gmail.com", "admin", "AQAAAAEAACcQAAAAEERS+U9fM02dgPmUiNM4fa3qQADt2IEjpO/SalcSxnLJeTHlONwqkiVekVv5Shjg3A==", "0912413908", true, 0, "", false, "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomFormats_FormatId",
                table: "Rooms",
                column: "FormatId",
                principalTable: "RoomFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomFormats_FormatId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomFormats",
                table: "RoomFormats");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("f357660a-9a84-4acb-8386-ff8cb211d746"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("03aeb1ff-1fe6-4fb0-b005-8c831db8dfe3"));

            migrationBuilder.RenameTable(
                name: "RoomFormats",
                newName: "Formats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Formats",
                table: "Formats",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("4883c8da-f8f9-447c-b762-09fe25b51291"), "5826aa5e-b482-4c12-8309-47c60efad68d", "Administrator role", "Admin", "Administrator" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4d7ac40b-9f15-4517-b8d5-25ac2e1113d0"), 0, "", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mistake4@gmail.com", true, "Hien", "Nguyen Thanh", false, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Mistakem4@gmail.com", "admin", "AQAAAAEAACcQAAAAENpfBjIQwAo106xbhOFzLYUyZ1LdotYVecC2s+qkSkRq7wXNF0ervw95mHjWkAv9mg==", "0912413908", true, 0, "", false, "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Formats_FormatId",
                table: "Rooms",
                column: "FormatId",
                principalTable: "Formats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
