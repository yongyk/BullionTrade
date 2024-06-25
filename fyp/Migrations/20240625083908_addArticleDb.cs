using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace fyp.Migrations
{
    /// <inheritdoc />
    public partial class addArticleDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "Content", "DateCreated", "Title" },
                values: new object[,]
                {
                    { 1, "Richard", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Vitae turpis massa sed elementum tempus egestas sed sed risus. Nunc vel risus commodo viverra maecenas accumsan lacus. Venenatis lectus magna fringilla urna. Faucibus turpis in eu mi bibendum neque. Augue eget arcu dictum varius duis at consectetur lorem donec. Nisl pretium fusce id velit. Diam in arcu cursus euismod quis viverra. Nunc sed augue lacus viverra vitae congue eu consequat. Auctor elit sed vulputate mi sit amet mauris. Est pellentesque elit ullamcorper dignissim cras tincidunt. Ut tristique et egestas quis ipsum suspendisse ultrices gravida dictum. Id nibh tortor id aliquet lectus proin nibh. Nisl rhoncus mattis rhoncus urna neque viverra justo nec. Amet nisl suscipit adipiscing bibendum est ultricies integer quis auctor. Augue mauris augue neque gravida in. Pharetra magna ac placerat vestibulum lectus mauris ultrices. Lobortis mattis aliquam faucibus purus in massa tempor.", "01/06/2024", " Gold & Silver will outperform stocks, Real Estate" },
                    { 2, "Ali", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Vitae turpis massa sed elementum tempus egestas sed sed risus. Nunc vel risus commodo viverra maecenas accumsan lacus. Venenatis lectus magna fringilla urna. Faucibus turpis in eu mi bibendum neque. Augue eget arcu dictum varius duis at consectetur lorem donec. Nisl pretium fusce id velit. Diam in arcu cursus euismod quis viverra. Nunc sed augue lacus viverra vitae congue eu consequat. Auctor elit sed vulputate mi sit amet mauris. Est pellentesque elit ullamcorper dignissim cras tincidunt. Ut tristique et egestas quis ipsum suspendisse ultrices gravida dictum. Id nibh tortor id aliquet lectus proin nibh. Nisl rhoncus mattis rhoncus urna neque viverra justo nec. Amet nisl suscipit adipiscing bibendum est ultricies integer quis auctor. Augue mauris augue neque gravida in. Pharetra magna ac placerat vestibulum lectus mauris ultrices. Lobortis mattis aliquam faucibus purus in massa tempor.", "02/06/2024", " Gold & Silver will outperform stocks, Real Estate 2" },
                    { 3, "Abu", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Vitae turpis massa sed elementum tempus egestas sed sed risus. Nunc vel risus commodo viverra maecenas accumsan lacus. Venenatis lectus magna fringilla urna. Faucibus turpis in eu mi bibendum neque. Augue eget arcu dictum varius duis at consectetur lorem donec. Nisl pretium fusce id velit. Diam in arcu cursus euismod quis viverra. Nunc sed augue lacus viverra vitae congue eu consequat. Auctor elit sed vulputate mi sit amet mauris. Est pellentesque elit ullamcorper dignissim cras tincidunt. Ut tristique et egestas quis ipsum suspendisse ultrices gravida dictum. Id nibh tortor id aliquet lectus proin nibh. Nisl rhoncus mattis rhoncus urna neque viverra justo nec. Amet nisl suscipit adipiscing bibendum est ultricies integer quis auctor. Augue mauris augue neque gravida in. Pharetra magna ac placerat vestibulum lectus mauris ultrices. Lobortis mattis aliquam faucibus purus in massa tempor.", "03/06/2024", " Gold & Silver will outperform stocks, Real Estate 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
