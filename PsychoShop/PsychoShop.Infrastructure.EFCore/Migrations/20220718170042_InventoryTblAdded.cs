﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsychoShop.Infrastructure.EFCore.Migrations
{
    public partial class InventoryTblAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Discounts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryOperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CurrentCount = table.Column<int>(type: "int", nullable: false),
                    OperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    Operation = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryOperations_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryOperations_InventoryId",
                table: "InventoryOperations",
                column: "InventoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryOperations");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Discounts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
