using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OTPService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CounterFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PrimaryCounter",
                table: "OTPUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OTPUsers_IssuedUserId",
                table: "OTPUsers",
                column: "IssuedUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OTPUsers_IssuedUserId",
                table: "OTPUsers");

            migrationBuilder.DropColumn(
                name: "PrimaryCounter",
                table: "OTPUsers");
        }
    }
}
