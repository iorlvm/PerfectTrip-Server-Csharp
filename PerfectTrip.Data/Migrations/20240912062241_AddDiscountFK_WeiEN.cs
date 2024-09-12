using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectTrip.Data.Migrations
{
    public partial class AddDiscountFK_WeiEN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscounts_CompanyId",
                table: "ProductDiscounts",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscounts_Companies_CompanyId",
                table: "ProductDiscounts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscounts_Companies_CompanyId",
                table: "ProductDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_ProductDiscounts_CompanyId",
                table: "ProductDiscounts");
        }
    }
}
