using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCFirstApp.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class CreatedCarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    YearOfConstruction = table.Column<int>(type: "int", nullable: false),
                    KilometresDriven = table.Column<int>(type: "int", nullable: false),
                    PowerInKilowatts = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Brand", "KilometresDriven", "PowerInKilowatts", "Price", "YearOfConstruction" },
                values: new object[,]
                {
                    { 1, "BMW", 106524, 136, 25000.0, 2014 },
                    { 2, "Mercedes", 196524, 128, 38000.0, 2016 },
                    { 3, "Seat", 326524, 77, 7500.0, 2013 },
                    { 4, "Skoda", 126524, 84, 3600.0, 2012 },
                    { 5, "Suzuki", 136524, 55, 500.0, 2004 },
                    { 6, "Citroen", 116524, 103, 2999.0, 2010 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");
        }
    }
}
