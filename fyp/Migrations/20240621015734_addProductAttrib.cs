using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fyp.Migrations
{
    /// <inheritdoc />
    public partial class addProductAttrib : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductBrand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductMetal",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductPurity",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ProductBrand", "ProductMetal", "ProductPurity", "Quantity" },
                values: new object[] { "Pamp", "Gold", "999.9", 300 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ProductBrand", "ProductMetal", "ProductPurity", "Quantity" },
                values: new object[] { "Pamp", "Gold", "999", 300 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ProductBrand", "ProductMetal", "ProductPurity", "Quantity" },
                values: new object[] { "Pamp", "Gold", "999", 300 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductBrand",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductMetal",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductPurity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");
        }
    }
}
