using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderNumber = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    SenderCity = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    SenderAddress = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    RecipientCity = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    RecipientAddress = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    CargoWeightKg = table.Column<decimal>(type: "TEXT", precision: 10, scale: 3, nullable: false),
                    PickupDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOrders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrders_CreatedAtUtc",
                table: "DeliveryOrders",
                column: "CreatedAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrders_OrderNumber",
                table: "DeliveryOrders",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrders_PickupDate",
                table: "DeliveryOrders",
                column: "PickupDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryOrders");
        }
    }
}
