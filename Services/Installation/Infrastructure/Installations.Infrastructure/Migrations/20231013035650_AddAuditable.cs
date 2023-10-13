using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Installations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "InstallationItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "InstallationItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "InstallationItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "InstallationItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "InstallationItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "InstallationItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Installation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Installation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Installation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Installation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Installation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "Installation",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "InstallationItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "InstallationItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "InstallationItem");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "InstallationItem");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "InstallationItem");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "InstallationItem");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Installation");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Installation");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Installation");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Installation");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Installation");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Installation");
        }
    }
}
