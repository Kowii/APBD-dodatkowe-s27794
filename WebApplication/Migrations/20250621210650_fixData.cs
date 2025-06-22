using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class fixData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prelegent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prelegent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uczestnik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uczestnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wydarzenie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    max_liczba_uczestnikow = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wydarzenie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prelegent_Wydarzenie",
                columns: table => new
                {
                    Prelegent_ID = table.Column<int>(type: "int", nullable: false),
                    Wydarzenie_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prelegent_Wydarzenie", x => new { x.Wydarzenie_ID, x.Prelegent_ID });
                    table.ForeignKey(
                        name: "FK_Prelegent_Wydarzenie_Prelegent_Prelegent_ID",
                        column: x => x.Prelegent_ID,
                        principalTable: "Prelegent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prelegent_Wydarzenie_Wydarzenie_Wydarzenie_ID",
                        column: x => x.Wydarzenie_ID,
                        principalTable: "Wydarzenie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uczestnik_Wydarzenie",
                columns: table => new
                {
                    Uczestnik_ID = table.Column<int>(type: "int", nullable: false),
                    Wydarzenie_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uczestnik_Wydarzenie", x => new { x.Wydarzenie_ID, x.Uczestnik_ID });
                    table.ForeignKey(
                        name: "FK_Uczestnik_Wydarzenie_Uczestnik_Uczestnik_ID",
                        column: x => x.Uczestnik_ID,
                        principalTable: "Uczestnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uczestnik_Wydarzenie_Wydarzenie_Wydarzenie_ID",
                        column: x => x.Wydarzenie_ID,
                        principalTable: "Wydarzenie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Prelegent",
                columns: new[] { "Id", "Imie", "Nazwisko" },
                values: new object[,]
                {
                    { 1, "Ferdynand", "Kiepski" },
                    { 2, "Marian", "Paździoch" },
                    { 3, "Arnold", "Boczek" }
                });

            migrationBuilder.InsertData(
                table: "Uczestnik",
                columns: new[] { "Id", "Imie", "Nazwisko" },
                values: new object[,]
                {
                    { 1, "Patryk", "Bejtman" },
                    { 2, "Anatoli", "Niebochód" },
                    { 3, "Yoshikage", "Kira" },
                    { 4, "Janusz", "Tracz" },
                    { 5, "Porucznik", "Kolombo" }
                });

            migrationBuilder.InsertData(
                table: "Wydarzenie",
                columns: new[] { "Id", "Data", "max_liczba_uczestnikow", "Opis", "Tytul" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, "Wielkie otwarcie parasola w Warszawie", "Otwarcie parasola" },
                    { 2, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Mikrokurs łączenia w pary w Krakowie", "Kurs parowania" }
                });

            migrationBuilder.InsertData(
                table: "Prelegent_Wydarzenie",
                columns: new[] { "Prelegent_ID", "Wydarzenie_ID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Uczestnik_Wydarzenie",
                columns: new[] { "Uczestnik_ID", "Wydarzenie_ID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prelegent_Wydarzenie_Prelegent_ID",
                table: "Prelegent_Wydarzenie",
                column: "Prelegent_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Uczestnik_Wydarzenie_Uczestnik_ID",
                table: "Uczestnik_Wydarzenie",
                column: "Uczestnik_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prelegent_Wydarzenie");

            migrationBuilder.DropTable(
                name: "Uczestnik_Wydarzenie");

            migrationBuilder.DropTable(
                name: "Prelegent");

            migrationBuilder.DropTable(
                name: "Uczestnik");

            migrationBuilder.DropTable(
                name: "Wydarzenie");
        }
    }
}
