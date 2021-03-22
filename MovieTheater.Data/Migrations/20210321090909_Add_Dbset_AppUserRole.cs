using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Data.Migrations
{
    public partial class Add_Dbset_AppUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLogins_AppUsers_UserId",
                table: "AppUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserLogins",
                table: "AppUserLogins");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("d95b91ce-63a6-4be1-9b59-5bd52d110287"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("22eae46b-540d-4836-a5d2-bbba8f3a3b09"));

            migrationBuilder.RenameTable(
                name: "AppUserLogins",
                newName: "AspNetUserLogins");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("2e015b35-8986-464d-8383-3bbdfdf8ebe6"), "da1a0920-75b0-4ae8-a629-091852408aac", "Administrator role", "Admin", "Administrator" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b98d47c3-59f1-488a-bd3c-fb373bf4e62f"), 0, "", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mistake4@gmail.com", true, "Hien", "Nguyen Thanh", false, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Mistakem4@gmail.com", "admin", "AQAAAAEAACcQAAAAEJbXzFt/AtZUcL6HTGKjVxowpoxsQ7782M9VCaTSVB1Uf+PYy0+oSAC/cktLEKaTrg==", "0912413908", true, "", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AppUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AppUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("2e015b35-8986-464d-8383-3bbdfdf8ebe6"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("b98d47c3-59f1-488a-bd3c-fb373bf4e62f"));

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AppUserLogins");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AppUserLogins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AppUserLogins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserLogins",
                table: "AppUserLogins",
                column: "UserId");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("d95b91ce-63a6-4be1-9b59-5bd52d110287"), "0a3384d9-e032-41a3-8180-3eb9ba439357", "Administrator role", "Admin", "Administrator" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("22eae46b-540d-4836-a5d2-bbba8f3a3b09"), 0, "", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mistake4@gmail.com", true, "Hien", "Nguyen Thanh", false, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Mistakem4@gmail.com", "admin", "AQAAAAEAACcQAAAAEBeeVgiccHzf8CKMWukO7mi6K5+hZN3xvYM32yRtrP89vslvZiNwp+hU68JhsP9Z2Q==", "0912413908", true, "", false, "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserLogins_AppUsers_UserId",
                table: "AppUserLogins",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
