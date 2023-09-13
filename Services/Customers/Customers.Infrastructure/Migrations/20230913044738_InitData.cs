using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Customers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "City", "CustomerName", "Email", "Phone", "PostalCode", "State" },
                values: new object[,]
                {
                    { new Guid("0a3d88e0-f6b2-47cd-aaa0-bbc1292f8939"), null, null, "Avis Gislason", null, null, null, null },
                    { new Guid("13bd7333-247f-4563-95e7-78e54a24aabf"), null, null, "Isac Torp", null, null, null, null },
                    { new Guid("456c2a0d-1e23-4ee6-81ce-16b814786501"), null, null, "Isidro VonRueden", null, null, null, null },
                    { new Guid("497c2b02-042c-4667-b232-dcc55216bb6a"), null, null, "Hillary Beer", null, null, null, null },
                    { new Guid("4b0eb601-c0f5-423e-88f9-f16fe893ce0b"), null, null, "Norma Maggio", null, null, null, null },
                    { new Guid("6c79c789-0212-48d6-aa4d-87e339e2090d"), null, null, "Shanna Labadie", null, null, null, null },
                    { new Guid("79c8e7f8-56e4-4b91-a969-6a9e37b22882"), null, null, "Sidney Hessel", null, null, null, null },
                    { new Guid("9953bfba-b380-48df-aa0a-33c28eb7ee74"), null, null, "Hosea Turner", null, null, null, null },
                    { new Guid("a527f245-694c-4893-982d-becaf29f0013"), null, null, "Litzy Cummerata", null, null, null, null },
                    { new Guid("a783af07-5704-4e93-8a5d-2983783f47b3"), null, null, "Dennis Homenick", null, null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("0a3d88e0-f6b2-47cd-aaa0-bbc1292f8939"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("13bd7333-247f-4563-95e7-78e54a24aabf"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("456c2a0d-1e23-4ee6-81ce-16b814786501"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("497c2b02-042c-4667-b232-dcc55216bb6a"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("4b0eb601-c0f5-423e-88f9-f16fe893ce0b"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("6c79c789-0212-48d6-aa4d-87e339e2090d"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("79c8e7f8-56e4-4b91-a969-6a9e37b22882"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("9953bfba-b380-48df-aa0a-33c28eb7ee74"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("a527f245-694c-4893-982d-becaf29f0013"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("a783af07-5704-4e93-8a5d-2983783f47b3"));
        }
    }
}
