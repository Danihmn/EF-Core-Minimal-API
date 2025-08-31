using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Perfumes.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaDadosIniciais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Perfumistas",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Verônica Kato" },
                    { 2, "Alberto Morillas" },
                    { 3, "Dominique Ropion" }
                });

            migrationBuilder.InsertData(
                table: "Perfumes",
                columns: new[] { "Id", "Marca", "Nome", "PerfumistaId", "Tipo", "Valor" },
                values: new object[,]
                {
                    { 1, "Natura", "Essencial Elixir", 1, "Deo Perfume", 230f },
                    { 2, "Calvin Klein", "CK One", 2, "Eau de Toilette", 199f },
                    { 3, "Dior", "Sauvage", 3, "Eau de Parfum", 550f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Perfumes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Perfumes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Perfumes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Perfumistas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Perfumistas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Perfumistas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
