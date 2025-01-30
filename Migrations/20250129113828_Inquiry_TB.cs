using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDealerWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Inquiry_TB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Make",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Inquiries");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_CarId",
                table: "Inquiries",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Cars_CarId",
                table: "Inquiries",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Cars_CarId",
                table: "Inquiries");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_CarId",
                table: "Inquiries");

            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "Inquiries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Inquiries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Inquiries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Inquiries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
