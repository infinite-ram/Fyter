using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fyter.Plugins.EFCoreSqlite.Migrations
{
    /// <inheritdoc />
    public partial class migration_20241218_125653 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OutdatedRequestCreatedAt",
                table: "Fighters",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "FighterRequests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEdited",
                table: "FighterRequests",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OutdatedRequestCreatedAt",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "FighterRequests");

            migrationBuilder.DropColumn(
                name: "DateEdited",
                table: "FighterRequests");
        }
    }
}
