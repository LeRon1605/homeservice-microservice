using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contracts.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeToContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contract_CustomerServiceRepId",
                table: "Contract",
                column: "CustomerServiceRepId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_SalePersonId",
                table: "Contract",
                column: "SalePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_SupervisorId",
                table: "Contract",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Employee_CustomerServiceRepId",
                table: "Contract",
                column: "CustomerServiceRepId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Employee_SalePersonId",
                table: "Contract",
                column: "SalePersonId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Employee_SupervisorId",
                table: "Contract",
                column: "SupervisorId",
                principalTable: "Employee",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Employee_CustomerServiceRepId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Employee_SalePersonId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Employee_SupervisorId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_CustomerServiceRepId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_SalePersonId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_SupervisorId",
                table: "Contract");
        }
    }
}
