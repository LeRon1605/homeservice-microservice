using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contracts.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ContractAction");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ContractAction");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ContractAction");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tax",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Tax",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Tax",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Tax",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Tax",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "Tax",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductUnit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductUnit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "ProductUnit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "ProductUnit",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProductUnit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "ProductUnit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Product",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PendingOrder",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PendingOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "PendingOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "PendingOrder",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "PendingOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "PendingOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PaymentMethod",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "PaymentMethod",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Material",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Material",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Material",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Material",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Material",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "Material",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Customer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Customer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ContractPayment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ContractLine",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ContractLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "ContractLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "ContractLine",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ContractLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "ContractLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Contract",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Contract",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProductUnit");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductUnit");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ProductUnit");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "ProductUnit");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProductUnit");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "ProductUnit");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PendingOrder");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PendingOrder");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "PendingOrder");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "PendingOrder");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "PendingOrder");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "PendingOrder");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ContractPayment");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ContractLine");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ContractLine");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ContractLine");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "ContractLine");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ContractLine");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "ContractLine");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Contract");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "ContractAction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserId",
                table: "ContractAction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ContractAction",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
