using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SQft",
                table: "Villas",
                newName: "Sqft");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Villas",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Villas",
                newName: "CreatedDate");

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2024, 6, 27, 12, 27, 41, 420, DateTimeKind.Utc).AddTicks(9062), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmastery.com/bluevillaimages/villa3.jpg", "Royal Villa", 4, 200.0, 550, new DateTime(2024, 6, 27, 12, 27, 41, 420, DateTimeKind.Utc).AddTicks(9064) },
                    { 2, "", new DateTime(2024, 6, 27, 12, 27, 41, 420, DateTimeKind.Utc).AddTicks(9069), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmastery.com/bluevillaimages/villa1.jpg", "Premium Pool Villa", 4, 300.0, 550, new DateTime(2024, 6, 27, 12, 27, 41, 420, DateTimeKind.Utc).AddTicks(9069) },
                    { 3, "", new DateTime(2024, 6, 27, 12, 27, 41, 420, DateTimeKind.Utc).AddTicks(9071), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmastery.com/bluevillaimages/villa4.jpg", "Luxury Pool Villa", 4, 400.0, 750, new DateTime(2024, 6, 27, 12, 27, 41, 420, DateTimeKind.Utc).AddTicks(9071) },
                    { 4, "", new DateTime(2024, 6, 27, 12, 27, 41, 420, DateTimeKind.Utc).AddTicks(9073), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmastery.com/bluevillaimages/villa5.jpg", "Diamond Villa", 4, 550.0, 900, new DateTime(2024, 6, 27, 12, 27, 41, 420, DateTimeKind.Utc).AddTicks(9073) },
                    { 5, "", new DateTime(2024, 6, 27, 12, 27, 41, 420, DateTimeKind.Utc).AddTicks(9074), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmastery.com/bluevillaimages/villa2.jpg", "Diamond Pool Villa", 4, 600.0, 1100, new DateTime(2024, 6, 27, 12, 27, 41, 420, DateTimeKind.Utc).AddTicks(9075) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "Sqft",
                table: "Villas",
                newName: "SQft");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Villas",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Villas",
                newName: "CreateDate");
        }
    }
}
