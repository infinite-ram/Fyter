using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fyter.Plugins.EFCoreSqlite.Migrations
{
    /// <inheritdoc />
    public partial class migration_20241127_184639 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenAdded",
                table: "FighterRequests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "HasBeenAdded", table: "FighterRequests");
        }
    }
}
