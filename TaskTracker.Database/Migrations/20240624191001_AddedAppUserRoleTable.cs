using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskTracker.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedAppUserRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "ApplicationUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApplicationUserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasAdminRights = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ApplicationUserRole",
                columns: new[] { "Id", "HasAdminRights", "Role" },
                values: new object[,]
                {
                    { 1, false, "Test User" },
                    { 2, false, "QA Tester" },
                    { 3, true, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_UserRoleId",
                table: "ApplicationUser",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_ApplicationUserRole_UserRoleId",
                table: "ApplicationUser",
                column: "UserRoleId",
                principalTable: "ApplicationUserRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_ApplicationUserRole_UserRoleId",
                table: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "ApplicationUserRole");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_UserRoleId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "ApplicationUser");
        }
    }
}
