using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fyter.WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration_20241201_163420 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestedRoles",
                table: "AspNetUsers",
                newName: "RequestedRolesSerialized"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestedRolesSerialized",
                table: "AspNetUsers",
                newName: "RequestedRoles"
            );
        }
    }
}
