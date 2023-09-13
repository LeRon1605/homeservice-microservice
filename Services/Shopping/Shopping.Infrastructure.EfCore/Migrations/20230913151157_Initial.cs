using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shopping.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("04b319f7-c834-46ff-9a8a-36b51b754937"), "Prince Schmitt" },
                    { new Guid("401b8f68-cfa4-4368-adba-f7ef4e385a09"), "Brionna Dooley" },
                    { new Guid("52e638ef-199b-44ab-a8ca-c77f7f2dccb9"), "Vergie Considine" },
                    { new Guid("5841c565-e264-432f-818b-3b92ef7e4c7e"), "Darlene Gerhold" },
                    { new Guid("64389ad9-328a-48bc-946b-9f25efbdde90"), "Kaleigh Collier" },
                    { new Guid("a98700ec-802b-46ff-873e-dbf120e949bf"), "Mose Cormier" },
                    { new Guid("ad73e28a-76a1-4419-824c-4e3328740e0b"), "Marshall Leffler" },
                    { new Guid("ae567ac6-daf1-4acc-b218-b79e18221852"), "Violet Mayer" },
                    { new Guid("d5ff163c-626d-4613-8581-aeba0a5f43a2"), "Deshawn Kub" },
                    { new Guid("ea953a63-c819-42db-bbe2-733bc98a7e6c"), "Graham Gutkowski" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
