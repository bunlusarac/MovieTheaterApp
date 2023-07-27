using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VenueService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeatingLayout",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LastRow = table.Column<char>(type: "character(1)", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatingLayout", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeatingState",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatingState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Location = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LayoutSeat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Row = table.Column<char>(type: "character(1)", nullable: false),
                    SeatNumber = table.Column<int>(type: "integer", nullable: false),
                    SeatType = table.Column<int>(type: "integer", nullable: false),
                    SeatingLayoutId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutSeat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LayoutSeat_SeatingLayout_SeatingLayoutId",
                        column: x => x.SeatingLayoutId,
                        principalTable: "SeatingLayout",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StateSeat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Row = table.Column<char>(type: "character(1)", nullable: false),
                    Occupied = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    SeatNumber = table.Column<int>(type: "integer", nullable: false),
                    SeatingStateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateSeat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateSeat_SeatingState_SeatingStateId",
                        column: x => x.SeatingStateId,
                        principalTable: "SeatingState",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Theaters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LayoutId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    VenueId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theaters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Theaters_SeatingLayout_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "SeatingLayout",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Theaters_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Localization = table.Column<int>(type: "integer", nullable: false),
                    MovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    SeatingStateId = table.Column<Guid>(type: "uuid", nullable: true),
                    TheaterId = table.Column<Guid>(type: "uuid", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Session_SeatingState_SeatingStateId",
                        column: x => x.SeatingStateId,
                        principalTable: "SeatingState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Session_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalTable: "Theaters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pricing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Price_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Price_Currency = table.Column<int>(type: "integer", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pricing_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LayoutSeat_SeatingLayoutId",
                table: "LayoutSeat",
                column: "SeatingLayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricing_SessionId",
                table: "Pricing",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_SeatingStateId",
                table: "Session",
                column: "SeatingStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_TheaterId",
                table: "Session",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_StateSeat_SeatingStateId",
                table: "StateSeat",
                column: "SeatingStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Theaters_LayoutId",
                table: "Theaters",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Theaters_VenueId",
                table: "Theaters",
                column: "VenueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LayoutSeat");

            migrationBuilder.DropTable(
                name: "Pricing");

            migrationBuilder.DropTable(
                name: "StateSeat");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "SeatingState");

            migrationBuilder.DropTable(
                name: "Theaters");

            migrationBuilder.DropTable(
                name: "SeatingLayout");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
