using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsychoShop.Infrastructure.EFCore.Migrations
{
    public partial class SeedDataForRoleAndAccountTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Roles_RoleId",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreationDate", "Name" },
                values: new object[] { 1, new DateTime(2022, 6, 26, 20, 40, 7, 958, DateTimeKind.Local).AddTicks(7974), "مدیر سایت" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreationDate", "Name" },
                values: new object[] { 2, new DateTime(2022, 6, 26, 20, 40, 7, 958, DateTimeKind.Local).AddTicks(7989), "کاربر عادی" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreationDate", "Email", "EmailConfirmed", "FullName", "MobilePhone", "Password", "ProfilePhoto", "RoleId", "Token", "UserName" },
                values: new object[] { 1, new DateTime(2022, 6, 26, 20, 40, 7, 958, DateTimeKind.Local).AddTicks(5926), "aspemail007@gmail.com", true, "مدیر سایت", "09876543210", "10000.YNmL5o6NPRQENvKVLgQaww==.enVFgrgZstDmlnWaXv7o/jjjL8e8F75AUgAk5ZmQEH4=", null, 1, "ihkecrTZxEe/wqmVG8wN/w==", "OwnerSite" });

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Roles_RoleId",
                table: "Accounts",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Roles_RoleId",
                table: "Accounts");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Roles_RoleId",
                table: "Accounts",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
