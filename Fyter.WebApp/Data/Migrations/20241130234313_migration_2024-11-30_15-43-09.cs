using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fyter.WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration_20241130_154309 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedFighterEditorPermission",
                table: "AspNetUsers"
            );

            migrationBuilder.DropColumn(
                name: "RequestedFighterEditorPermission",
                table: "AspNetUsers"
            );

            migrationBuilder.AddColumn<string>(
                name: "RequestedRoles",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "RequestedRoles", table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedFighterEditorPermission",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false
            );

            migrationBuilder.AddColumn<bool>(
                name: "RequestedFighterEditorPermission",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false
            );
        }
    }
}
