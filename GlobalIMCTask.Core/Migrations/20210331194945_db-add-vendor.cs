using Microsoft.EntityFrameworkCore.Migrations;

namespace GlobalIMCTask.Core.Migrations
{
    public partial class dbaddvendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VendorUID",
                table: "Products",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VendorUID",
                table: "Products");
        }
    }
}
