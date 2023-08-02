using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoyaltyService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IndexMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyCustomers_CustomerId",
                table: "LoyaltyCustomers",
                column: "CustomerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoyaltyCustomers_CustomerId",
                table: "LoyaltyCustomers");
        }
    }
}
