using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class SellPriceNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SellPrice",
                table: "Products",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SellPrice",
                table: "Products",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);
        }
    }
}
