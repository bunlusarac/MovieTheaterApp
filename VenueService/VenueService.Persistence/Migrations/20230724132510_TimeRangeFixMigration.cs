﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VenueService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TimeRangeFixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Session",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Session");
        }
    }
}
