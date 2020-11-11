﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SaskPartyDonors.Data.Migrations
{
    public partial class AddSourceAndCreatedAtToContribution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Contributions",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "Source",
                table: "Contributions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Contributions");
        }
    }
}
