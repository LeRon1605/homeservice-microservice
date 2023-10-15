using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contracts.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddTaxValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Tax",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TaxValue",
                table: "ContractLine",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "TaxValue",
                table: "ContractLine");
        }
    }
}
