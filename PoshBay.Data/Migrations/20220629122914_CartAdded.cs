using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoshBay.Data.Migrations
{
    public partial class CartAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ShoppingCarts",
                newName: "ShoppingCartId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Carts",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartId",
                table: "ShoppingCarts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Carts",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Carts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
