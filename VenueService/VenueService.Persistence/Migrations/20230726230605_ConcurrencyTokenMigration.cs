using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VenueService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConcurrencyTokenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "StateSeat",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xmin",
                table: "StateSeat");
        }
    }
}
