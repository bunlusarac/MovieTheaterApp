using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VenueService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeletionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pricing_Session_SessionId",
                table: "Pricing");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_SeatingState_SeatingStateId",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Theaters_TheaterId",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_StateSeat_SeatingState_SeatingStateId",
                table: "StateSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_Theaters_SeatingLayout_LayoutId",
                table: "Theaters");

            migrationBuilder.DropIndex(
                name: "IX_Theaters_LayoutId",
                table: "Theaters");

            migrationBuilder.DropIndex(
                name: "IX_Session_SeatingStateId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "LayoutId",
                table: "Theaters");

            migrationBuilder.DropColumn(
                name: "SeatingStateId",
                table: "Session");

            migrationBuilder.AddColumn<Guid>(
                name: "SessionId",
                table: "SeatingState",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TheaterId",
                table: "SeatingLayout",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SeatingState_SessionId",
                table: "SeatingState",
                column: "SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeatingLayout_TheaterId",
                table: "SeatingLayout",
                column: "TheaterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pricing_Session_SessionId",
                table: "Pricing",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatingLayout_Theaters_TheaterId",
                table: "SeatingLayout",
                column: "TheaterId",
                principalTable: "Theaters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatingState_Session_SessionId",
                table: "SeatingState",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Theaters_TheaterId",
                table: "Session",
                column: "TheaterId",
                principalTable: "Theaters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StateSeat_SeatingState_SeatingStateId",
                table: "StateSeat",
                column: "SeatingStateId",
                principalTable: "SeatingState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pricing_Session_SessionId",
                table: "Pricing");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatingLayout_Theaters_TheaterId",
                table: "SeatingLayout");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatingState_Session_SessionId",
                table: "SeatingState");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Theaters_TheaterId",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_StateSeat_SeatingState_SeatingStateId",
                table: "StateSeat");

            migrationBuilder.DropIndex(
                name: "IX_SeatingState_SessionId",
                table: "SeatingState");

            migrationBuilder.DropIndex(
                name: "IX_SeatingLayout_TheaterId",
                table: "SeatingLayout");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "SeatingState");

            migrationBuilder.DropColumn(
                name: "TheaterId",
                table: "SeatingLayout");

            migrationBuilder.AddColumn<Guid>(
                name: "LayoutId",
                table: "Theaters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SeatingStateId",
                table: "Session",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Theaters_LayoutId",
                table: "Theaters",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_SeatingStateId",
                table: "Session",
                column: "SeatingStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pricing_Session_SessionId",
                table: "Pricing",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_SeatingState_SeatingStateId",
                table: "Session",
                column: "SeatingStateId",
                principalTable: "SeatingState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Theaters_TheaterId",
                table: "Session",
                column: "TheaterId",
                principalTable: "Theaters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StateSeat_SeatingState_SeatingStateId",
                table: "StateSeat",
                column: "SeatingStateId",
                principalTable: "SeatingState",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Theaters_SeatingLayout_LayoutId",
                table: "Theaters",
                column: "LayoutId",
                principalTable: "SeatingLayout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
