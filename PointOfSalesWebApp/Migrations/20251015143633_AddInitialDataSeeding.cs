using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSalesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Active", "CategoryName", "CreatedAt", "DeletedAt", "Description", "UpdatedAt" },
                values: new object[] { 1L, true, "Makanan ringan", new DateTime(2025, 10, 15, 14, 36, 31, 892, DateTimeKind.Utc).AddTicks(4746), null, null, null });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Active", "CreatedAt", "DeletedAt", "Email", "FullName", "JoinDate", "PhoneNumber", "UpdatedAt" },
                values: new object[] { 1L, true, new DateTime(2025, 10, 15, 14, 36, 31, 892, DateTimeKind.Utc).AddTicks(5035), null, "budi@example.com", "Budi Santoso", new DateTime(2025, 10, 15, 14, 36, 31, 892, DateTimeKind.Utc).AddTicks(5034), null, null });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "ProductId", "TagName", "UpdatedAt" },
                values: new object[] { 1L, new DateTime(2025, 10, 15, 14, 36, 31, 892, DateTimeKind.Utc).AddTicks(4983), null, null, "Best Seller", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Active", "CategoryId", "CreatedAt", "DeletedAt", "Price", "ProductName", "Stock", "UpdatedAt" },
                values: new object[] { 1L, true, 1L, new DateTime(2025, 10, 15, 21, 36, 31, 892, DateTimeKind.Local).AddTicks(5011), null, 15000m, "Keripik Kentang Original", 100, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
