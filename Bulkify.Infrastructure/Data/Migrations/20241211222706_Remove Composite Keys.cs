using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulkify.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCompositeKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductRates",
                table: "ProductRates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerPurchases",
                table: "CustomerPurchases");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductRates",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CustomerPurchases",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductRates",
                table: "ProductRates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerPurchases",
                table: "CustomerPurchases",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRates_CustomerId_ProductId",
                table: "ProductRates",
                columns: new[] { "CustomerId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPurchases_PurchaseId_CustomerId",
                table: "CustomerPurchases",
                columns: new[] { "PurchaseId", "CustomerId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductRates",
                table: "ProductRates");

            migrationBuilder.DropIndex(
                name: "IX_ProductRates_CustomerId_ProductId",
                table: "ProductRates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerPurchases",
                table: "CustomerPurchases");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPurchases_PurchaseId_CustomerId",
                table: "CustomerPurchases");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductRates");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CustomerPurchases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductRates",
                table: "ProductRates",
                columns: new[] { "CustomerId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerPurchases",
                table: "CustomerPurchases",
                columns: new[] { "PurchaseId", "CustomerId" });
        }
    }
}
