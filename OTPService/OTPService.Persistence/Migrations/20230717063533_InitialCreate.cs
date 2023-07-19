using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OTPService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OTPUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BlockTimeout = table.Column<TimeSpan>(type: "interval", nullable: false),
                    FailedAttempts = table.Column<int>(type: "integer", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    IsDisposed = table.Column<bool>(type: "boolean", nullable: false),
                    IssuedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MfaEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    OtpExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PrimarySecret = table.Column<byte[]>(type: "bytea", nullable: false),
                    SecondaryCounter = table.Column<long>(type: "bigint", nullable: false),
                    SecondarySecret = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPUsers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OTPUsers");
        }
    }
}
