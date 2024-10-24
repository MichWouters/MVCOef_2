using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCOef_2.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "Id", "AantalPlaatsen", "EindDatum", "IsBadge", "IsKleding", "IsWerkschoenen", "Locatie", "Omschrijving", "StartDatum" },
                values: new object[,]
                {
                    { 1, 5, new DateTime(2024, 10, 25, 10, 31, 28, 249, DateTimeKind.Local).AddTicks(260), true, false, false, "Turnhout", "Leraar positie", new DateTime(2024, 10, 24, 10, 31, 28, 249, DateTimeKind.Local).AddTicks(262) },
                    { 2, 1, new DateTime(2024, 10, 31, 10, 31, 28, 249, DateTimeKind.Local).AddTicks(269), true, true, true, "Lier", "Security guard", new DateTime(2024, 10, 24, 10, 31, 28, 249, DateTimeKind.Local).AddTicks(270) }
                });

            migrationBuilder.InsertData(
                table: "Klant",
                columns: new[] { "Id", "Bankrekeningnummer", "Gemeente", "Huisnummer", "Naam", "Postcode", "Straat", "Voornaam" },
                values: new object[,]
                {
                    { "1", "123", "Turnhout", "51", "De magazijnier", "3540", "Steenweg", "Jos" },
                    { "2", "wadfe", "Lier", "51", "De businessman", "3540", "Steenweg", "Frank" }
                });

            migrationBuilder.InsertData(
                table: "KlantJob",
                columns: new[] { "Id", "JobId", "KlantId" },
                values: new object[] { 1, 2, "1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Klant",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "KlantJob",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Klant",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}
