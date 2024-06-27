using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedApplicationUserRoleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserRoleId",
                table: "ApplicationUser",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserRoleId",
                table: "ApplicationUser");
        }
    }
}
