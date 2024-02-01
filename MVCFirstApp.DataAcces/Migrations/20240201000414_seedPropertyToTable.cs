using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCFirstApp.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class seedPropertyToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Session",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Session",
                table: "OrderHeaders");
        }
    }
}
