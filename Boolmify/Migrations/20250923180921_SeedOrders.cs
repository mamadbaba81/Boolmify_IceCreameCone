using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Boolmify.Migrations
{
    /// <inheritdoc />
    public partial class SeedOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CreateAt", "DeliveryDate", "DiscountAmount", "FinalAmouont", "RecipientAddress", "RecipientName", "RecipientPhone", "Status", "TotalAmount", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 50000m, 450000m, "تهران، خیابان آزادی، پلاک 101", "علی محمدی", "09120000001", 0, 500000m, 1 },
                    { 2, new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 50000m, 700000m, "اصفهان، میدان نقش جهان، کوچه 12", "زهرا رضایی", "09120000002", 1, 750000m, 2 },
                    { 3, new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 1200000m, "مشهد، خیابان امام رضا، پلاک 45", "محمد احمدی", "09120000003", 2, 1200000m, 1002 },
                    { 4, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 30000m, 270000m, "شیراز، خیابان زند، کوچه 3", "سارا کیانی", "09120000004", 3, 300000m, 1003 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 4);
        }
    }
}
