using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fyp.Migrations
{
    /// <inheritdoc />
    public partial class updateSelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellings_AspNetUsers_ApplicationUserId",
                table: "Sellings");

            migrationBuilder.DropIndex(
                name: "IX_Sellings_ApplicationUserId",
                table: "Sellings");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Sellings");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Sellings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Sellings");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Sellings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sellings_ApplicationUserId",
                table: "Sellings",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellings_AspNetUsers_ApplicationUserId",
                table: "Sellings",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
