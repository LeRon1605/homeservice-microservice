using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contracts.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddActionByEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ContractAction_ActionByEmployeeId",
                table: "ContractAction",
                column: "ActionByEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractAction_Employee_ActionByEmployeeId",
                table: "ContractAction",
                column: "ActionByEmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractAction_Employee_ActionByEmployeeId",
                table: "ContractAction");

            migrationBuilder.DropIndex(
                name: "IX_ContractAction_ActionByEmployeeId",
                table: "ContractAction");
        }
    }
}
