using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCFirstApp.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class addInitialCompanyRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Country", "Name", "PhoneNumber", "PostalCode", "StreetAdress" },
                values: new object[,]
                {
                    { 1, "Presov", "Slovakia", "Tech Solutions", "666999696969", "08001", "PFB1" },
                    { 2, "Presov", "Slovakia", "Rear Differentials Kingdom", "666999696969", "08001", "PFB1" },
                    { 3, "Presov", "Slovakia", "White Horse Group", "666999696969", "08001", "PFB1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
