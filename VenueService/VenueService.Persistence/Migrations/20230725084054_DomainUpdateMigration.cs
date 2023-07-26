using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VenueService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DomainUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_SeatingState_SeatingStateId",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_Theaters_SeatingLayout_LayoutId",
                table: "Theaters");

            migrationBuilder.AlterColumn<Guid>(
                name: "LayoutId",
                table: "Theaters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Session",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SeatingStateId",
                table: "Session",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Session",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_SeatingState_SeatingStateId",
                table: "Session",
                column: "SeatingStateId",
                principalTable: "SeatingState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Theaters_SeatingLayout_LayoutId",
                table: "Theaters",
                column: "LayoutId",
                principalTable: "SeatingLayout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_SeatingState_SeatingStateId",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_Theaters_SeatingLayout_LayoutId",
                table: "Theaters");

            migrationBuilder.AlterColumn<Guid>(
                name: "LayoutId",
                table: "Theaters",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Session",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "SeatingStateId",
                table: "Session",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Session",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_SeatingState_SeatingStateId",
                table: "Session",
                column: "SeatingStateId",
                principalTable: "SeatingState",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Theaters_SeatingLayout_LayoutId",
                table: "Theaters",
                column: "LayoutId",
                principalTable: "SeatingLayout",
                principalColumn: "Id");
        }
    }
}
