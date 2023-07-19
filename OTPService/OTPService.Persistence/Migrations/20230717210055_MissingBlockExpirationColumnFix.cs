using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OTPService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MissingBlockExpirationColumnFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BlockExpirationDate",
                table: "OTPUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockExpirationDate",
                table: "OTPUsers");
        }
    }
}
