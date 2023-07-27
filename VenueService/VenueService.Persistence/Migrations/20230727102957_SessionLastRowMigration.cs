using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VenueService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SessionLastRowMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<char>(
                name: "LastRow",
                table: "SeatingState",
                type: "character(1)",
                nullable: false,
                defaultValue: 'A');
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastRow",
                table: "SeatingState");
        }
    }
}
