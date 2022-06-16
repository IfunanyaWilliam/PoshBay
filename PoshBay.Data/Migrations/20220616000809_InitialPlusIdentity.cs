using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoshBay.Data.Migrations
{
    public partial class InitialPlusIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AspNetUsers",
                newName: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "AspNetUsers",
                newName: "UserId");
        }
    }
}
