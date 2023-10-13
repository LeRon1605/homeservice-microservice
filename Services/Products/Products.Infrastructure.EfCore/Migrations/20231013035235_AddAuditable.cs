using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_ProductType_ProductTypeId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_ProductUnit_SellUnitId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroup_ProductGroupId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductType_ProductTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductUnit_BuyUnitId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductUnit_SellUnitId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "Material");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SellUnitId",
                table: "Product",
                newName: "IX_Product_SellUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductTypeId",
                table: "Product",
                newName: "IX_Product_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductGroupId",
                table: "Product",
                newName: "IX_Product_ProductGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductCode",
                table: "Product",
                newName: "IX_Product_ProductCode");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BuyUnitId",
                table: "Product",
                newName: "IX_Product_BuyUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_SellUnitId",
                table: "Material",
                newName: "IX_Material_SellUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_ProductTypeId",
                table: "Material",
                newName: "IX_Material_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_MaterialCode",
                table: "Material",
                newName: "IX_Material_MaterialCode");

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
                table: "ProductType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "ProductType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "ProductType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProductType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "ProductType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "ProductImage",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductGroup",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductGroup",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "ProductGroup",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "ProductGroup",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProductGroup",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "ProductGroup",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                table: "Material",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_ProductType_ProductTypeId",
                table: "Material",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_ProductUnit_SellUnitId",
                table: "Material",
                column: "SellUnitId",
                principalTable: "ProductUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductGroup_ProductGroupId",
                table: "Product",
                column: "ProductGroupId",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductUnit_BuyUnitId",
                table: "Product",
                column: "BuyUnitId",
                principalTable: "ProductUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductUnit_SellUnitId",
                table: "Product",
                column: "SellUnitId",
                principalTable: "ProductUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_ProductType_ProductTypeId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_ProductUnit_SellUnitId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductGroup_ProductGroupId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductUnit_BuyUnitId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductUnit_SellUnitId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                table: "Material");

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
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "ProductGroup");

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

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "Materials");

            migrationBuilder.RenameIndex(
                name: "IX_Product_SellUnitId",
                table: "Products",
                newName: "IX_Products_SellUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductTypeId",
                table: "Products",
                newName: "IX_Products_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductGroupId",
                table: "Products",
                newName: "IX_Products_ProductGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductCode",
                table: "Products",
                newName: "IX_Products_ProductCode");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BuyUnitId",
                table: "Products",
                newName: "IX_Products_BuyUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_SellUnitId",
                table: "Materials",
                newName: "IX_Materials_SellUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_ProductTypeId",
                table: "Materials",
                newName: "IX_Materials_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_MaterialCode",
                table: "Materials",
                newName: "IX_Materials_MaterialCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_ProductType_ProductTypeId",
                table: "Materials",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_ProductUnit_SellUnitId",
                table: "Materials",
                column: "SellUnitId",
                principalTable: "ProductUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroup_ProductGroupId",
                table: "Products",
                column: "ProductGroupId",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductType_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductUnit_BuyUnitId",
                table: "Products",
                column: "BuyUnitId",
                principalTable: "ProductUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductUnit_SellUnitId",
                table: "Products",
                column: "SellUnitId",
                principalTable: "ProductUnit",
                principalColumn: "Id");
        }
    }
}
