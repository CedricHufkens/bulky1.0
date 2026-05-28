using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bulky.Migrations
{
    /// <inheritdoc />
    public partial class Meerdereboeken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boeken",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boeken", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Boeken",
                columns: new[] { "ID", "DisplayOrder", "Naam" },
                values: new object[,]
                {
                    { 1, 1, "Incincible" },
                    { 2, 2, "Cedric de goat" },
                    { 3, 3, "The walking dead" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boeken");
        }
    }
}
