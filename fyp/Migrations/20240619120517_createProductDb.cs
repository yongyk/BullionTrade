using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace fyp.Migrations
{
    /// <inheritdoc />
    public partial class createProductDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tortor pretium viverra suspendisse potenti. Condimentum vitae sapien pellentesque habitant morbi. Aliquam ultrices sagittis orci a scelerisque purus. Amet cursus sit amet dictum sit amet justo. Auctor urna nunc id cursus metus. Magnis dis parturient montes nascetur ridiculus mus. Mauris rhoncus aenean vel elit. Commodo sed egestas egestas fringilla phasellus faucibus scelerisque eleifend. Dictum sit amet justo donec enim diam. Tristique senectus et netus et malesuada fames ac turpis egestas. Mauris commodo quis imperdiet massa tincidunt nunc pulvinar. Dignissim convallis aenean et tortor at. Ut eu sem integer vitae justo. Sapien pellentesque habitant morbi tristique. Nisl purus in mollis nunc sed id semper risus in.", "pamp gold bar 10 g", 5000.0 },
                    { 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tortor pretium viverra suspendisse potenti. Condimentum vitae sapien pellentesque habitant morbi. Aliquam ultrices sagittis orci a scelerisque purus. Amet cursus sit amet dictum sit amet justo. Auctor urna nunc id cursus metus. Magnis dis parturient montes nascetur ridiculus mus. Mauris rhoncus aenean vel elit. Commodo sed egestas egestas fringilla phasellus faucibus scelerisque eleifend. Dictum sit amet justo donec enim diam. Tristique senectus et netus et malesuada fames ac turpis egestas. Mauris commodo quis imperdiet massa tincidunt nunc pulvinar. Dignissim convallis aenean et tortor at. Ut eu sem integer vitae justo. Sapien pellentesque habitant morbi tristique. Nisl purus in mollis nunc sed id semper risus in.", "pamp gold bar 50 g", 50000.0 },
                    { 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tortor pretium viverra suspendisse potenti. Condimentum vitae sapien pellentesque habitant morbi. Aliquam ultrices sagittis orci a scelerisque purus. Amet cursus sit amet dictum sit amet justo. Auctor urna nunc id cursus metus. Magnis dis parturient montes nascetur ridiculus mus. Mauris rhoncus aenean vel elit. Commodo sed egestas egestas fringilla phasellus faucibus scelerisque eleifend. Dictum sit amet justo donec enim diam. Tristique senectus et netus et malesuada fames ac turpis egestas. Mauris commodo quis imperdiet massa tincidunt nunc pulvinar. Dignissim convallis aenean et tortor at. Ut eu sem integer vitae justo. Sapien pellentesque habitant morbi tristique. Nisl purus in mollis nunc sed id semper risus in.", "pamp gold bar 5 g", 500.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
