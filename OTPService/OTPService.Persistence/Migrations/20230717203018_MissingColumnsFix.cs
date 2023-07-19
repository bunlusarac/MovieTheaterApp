using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OTPService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MissingColumnsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisposedAttempts",
                table: "OTPUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxDisposals",
                table: "OTPUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxRetries",
                table: "OTPUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OtpTimeWindow",
                table: "OTPUsers",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisposedAttempts",
                table: "OTPUsers");

            migrationBuilder.DropColumn(
                name: "MaxDisposals",
                table: "OTPUsers");

            migrationBuilder.DropColumn(
                name: "MaxRetries",
                table: "OTPUsers");

            migrationBuilder.DropColumn(
                name: "OtpTimeWindow",
                table: "OTPUsers");
        }
    }
}
