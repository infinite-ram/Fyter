using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fyter.WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration_20241201_172814 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "RequestedRolesSerialized", table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserRequestedRoles",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequestedRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRequestedRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_UserRequestedRoles_UserId",
                table: "UserRequestedRoles",
                column: "UserId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "UserRequestedRoles");

            migrationBuilder.AddColumn<string>(
                name: "RequestedRolesSerialized",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: ""
            );
        }
    }
}
