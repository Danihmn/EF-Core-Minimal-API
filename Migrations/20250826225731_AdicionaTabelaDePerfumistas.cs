using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perfumes.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaTabelaDePerfumistas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PerfumistaId",
                table: "Perfumes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Perfumistas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfumistas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Perfumes_PerfumistaId",
                table: "Perfumes",
                column: "PerfumistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Perfumes_Perfumistas_PerfumistaId",
                table: "Perfumes",
                column: "PerfumistaId",
                principalTable: "Perfumistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Perfumes_Perfumistas_PerfumistaId",
                table: "Perfumes");

            migrationBuilder.DropTable(
                name: "Perfumistas");

            migrationBuilder.DropIndex(
                name: "IX_Perfumes_PerfumistaId",
                table: "Perfumes");

            migrationBuilder.DropColumn(
                name: "PerfumistaId",
                table: "Perfumes");
        }
    }
}
