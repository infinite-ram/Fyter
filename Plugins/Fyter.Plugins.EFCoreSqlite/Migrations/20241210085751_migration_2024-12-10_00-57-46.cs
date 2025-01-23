using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fyter.Plugins.EFCoreSqlite.Migrations
{
    /// <inheritdoc />
    public partial class migration_20241210_005746 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OutdatedByUserId",
                table: "Fighters",
                type: "TEXT",
                maxLength: 450,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OutdatedByUserId",
                table: "Fighters");
        }
    }
}
