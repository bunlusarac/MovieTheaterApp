using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VenueService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IsRequiredMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LayoutSeat_SeatingLayout_SeatingLayoutId",
                table: "LayoutSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_Theaters_Venues_VenueId",
                table: "Theaters");

            migrationBuilder.AlterColumn<Guid>(
                name: "VenueId",
                table: "Theaters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SeatingStateId",
                table: "StateSeat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TheaterId",
                table: "Session",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SessionId",
                table: "Pricing",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SeatingLayoutId",
                table: "LayoutSeat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LayoutSeat_SeatingLayout_SeatingLayoutId",
                table: "LayoutSeat",
                column: "SeatingLayoutId",
                principalTable: "SeatingLayout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Theaters_Venues_VenueId",
                table: "Theaters",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LayoutSeat_SeatingLayout_SeatingLayoutId",
                table: "LayoutSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_Theaters_Venues_VenueId",
                table: "Theaters");

            migrationBuilder.AlterColumn<Guid>(
                name: "VenueId",
                table: "Theaters",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SeatingStateId",
                table: "StateSeat",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TheaterId",
                table: "Session",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SessionId",
                table: "Pricing",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SeatingLayoutId",
                table: "LayoutSeat",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_LayoutSeat_SeatingLayout_SeatingLayoutId",
                table: "LayoutSeat",
                column: "SeatingLayoutId",
                principalTable: "SeatingLayout",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Theaters_Venues_VenueId",
                table: "Theaters",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id");
        }
    }
}
