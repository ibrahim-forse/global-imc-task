using Microsoft.EntityFrameworkCore.Migrations;

namespace GlobalIMCTask.Core.Migrations
{
    public partial class dbeditrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietaryTypes_Products_ProductId",
                table: "DietaryTypes");

            migrationBuilder.DropIndex(
                name: "IX_DietaryTypes_ProductId",
                table: "DietaryTypes");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "DietaryTypes");

            migrationBuilder.CreateTable(
                name: "DietaryTypeProduct",
                columns: table => new
                {
                    DietaryTypesId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryTypeProduct", x => new { x.DietaryTypesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_DietaryTypeProduct_DietaryTypes_DietaryTypesId",
                        column: x => x.DietaryTypesId,
                        principalTable: "DietaryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietaryTypeProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietaryTypeProduct_ProductsId",
                table: "DietaryTypeProduct",
                column: "ProductsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietaryTypeProduct");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "DietaryTypes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DietaryTypes_ProductId",
                table: "DietaryTypes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietaryTypes_Products_ProductId",
                table: "DietaryTypes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
