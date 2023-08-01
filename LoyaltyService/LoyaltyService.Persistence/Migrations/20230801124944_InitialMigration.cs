using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoyaltyService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoyaltyCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Redeems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoyaltyCustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    RedeemDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Transaction_Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Redeems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Redeems_LoyaltyCustomers_LoyaltyCustomerId",
                        column: x => x.LoyaltyCustomerId,
                        principalTable: "LoyaltyCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoyaltyCustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    PointsBalance_Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_LoyaltyCustomers_LoyaltyCustomerId",
                        column: x => x.LoyaltyCustomerId,
                        principalTable: "LoyaltyCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Redeems_LoyaltyCustomerId",
                table: "Redeems",
                column: "LoyaltyCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_LoyaltyCustomerId",
                table: "Wallets",
                column: "LoyaltyCustomerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Redeems");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "LoyaltyCustomers");
        }
    }
}
