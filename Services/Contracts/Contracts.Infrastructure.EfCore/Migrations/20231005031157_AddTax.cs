using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contracts.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddTax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TaxId",
                table: "ContractLine",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxName",
                table: "ContractLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractLine_TaxId",
                table: "ContractLine",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractLine_Tax_TaxId",
                table: "ContractLine",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractLine_Tax_TaxId",
                table: "ContractLine");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropIndex(
                name: "IX_ContractLine_TaxId",
                table: "ContractLine");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "ContractLine");

            migrationBuilder.DropColumn(
                name: "TaxName",
                table: "ContractLine");
        }
    }
}
