using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddProductUnitIdToOrderLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductUnitId",
                table: "OrderLine",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_ProductUnitId",
                table: "OrderLine",
                column: "ProductUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_ProductUnit_ProductUnitId",
                table: "OrderLine",
                column: "ProductUnitId",
                principalTable: "ProductUnit",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_ProductUnit_ProductUnitId",
                table: "OrderLine");

            migrationBuilder.DropIndex(
                name: "IX_OrderLine_ProductUnitId",
                table: "OrderLine");

            migrationBuilder.DropColumn(
                name: "ProductUnitId",
                table: "OrderLine");
        }
    }
}
