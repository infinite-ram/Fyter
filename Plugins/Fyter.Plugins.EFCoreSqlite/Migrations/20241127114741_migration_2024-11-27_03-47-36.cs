using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fyter.Plugins.EFCoreSqlite.Migrations
{
    /// <inheritdoc />
    public partial class migration_20241127_034736 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "Name", table: "Fighters", newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Fighters",
                type: "TEXT",
                nullable: false,
                defaultValue: ""
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "FirstName", table: "Fighters");

            migrationBuilder.RenameColumn(name: "LastName", table: "Fighters", newName: "Name");
        }
    }
}
