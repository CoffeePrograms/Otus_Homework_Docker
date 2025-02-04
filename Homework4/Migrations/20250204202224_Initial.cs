using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Homework4.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherForecast",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TemperatureC = table.Column<int>(type: "integer", nullable: false),
                    Summary = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecast", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WeatherForecast",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[,]
                {
                    { new Guid("07daf33a-638e-4737-8612-4a847c6c047f"), new DateOnly(2025, 2, 6), "Freezing", 7 },
                    { new Guid("2be2119d-bf85-46d3-a414-02efc55dc8c6"), new DateOnly(2025, 2, 7), "Scorching", -2 },
                    { new Guid("579fab4b-a0b0-4bfc-b4f9-05c2c37a7a20"), new DateOnly(2025, 2, 5), "Bracing", -17 },
                    { new Guid("5c8f5ed6-bc06-4cd8-8652-5d045e7623e1"), new DateOnly(2025, 2, 9), "Scorching", 41 },
                    { new Guid("e6702e34-6195-4609-ac87-4bab2705a70e"), new DateOnly(2025, 2, 8), "Scorching", -1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecast");
        }
    }
}
