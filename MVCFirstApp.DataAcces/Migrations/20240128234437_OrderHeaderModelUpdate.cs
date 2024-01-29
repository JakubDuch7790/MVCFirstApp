using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCFirstApp.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class OrderHeaderModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderToral",
                table: "OrderHeaders");

            migrationBuilder.AddColumn<double>(
                name: "OrderTotal",
                table: "OrderHeaders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTotal",
                table: "OrderHeaders");

            migrationBuilder.AddColumn<int>(
                name: "OrderToral",
                table: "OrderHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
