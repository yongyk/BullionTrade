using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fyp.Migrations
{
    /// <inheritdoc />
    public partial class updateSelling4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentSlotId",
                table: "Sellings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sellings_AppointmentSlotId",
                table: "Sellings",
                column: "AppointmentSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellings_AppointmentSlots_AppointmentSlotId",
                table: "Sellings",
                column: "AppointmentSlotId",
                principalTable: "AppointmentSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellings_AppointmentSlots_AppointmentSlotId",
                table: "Sellings");

            migrationBuilder.DropIndex(
                name: "IX_Sellings_AppointmentSlotId",
                table: "Sellings");

            migrationBuilder.DropColumn(
                name: "AppointmentSlotId",
                table: "Sellings");
        }
    }
}
