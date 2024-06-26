using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Database.Migrations
{
    /// <inheritdoc />
    public partial class AnotherCleanUpOnTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_ApplicationUserRole_UserRoleId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPassword_ApplicationUser_ApplicationUserId",
                table: "ApplicationUserPassword");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserPassword_ApplicationUserId",
                table: "ApplicationUserPassword");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_UserRoleId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ApplicationUserPassword");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<Guid>(
                name: "UserPasswordId",
                table: "ApplicationUser",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_UserPasswordId",
                table: "ApplicationUser",
                column: "UserPasswordId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_ApplicationUserPassword_UserPasswordId",
                table: "ApplicationUser",
                column: "UserPasswordId",
                principalTable: "ApplicationUserPassword",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_ApplicationUserPassword_UserPasswordId",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_UserPasswordId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "UserPasswordId",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "ApplicationUserPassword",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "ApplicationUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPassword_ApplicationUserId",
                table: "ApplicationUserPassword",
                column: "ApplicationUserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPassword_ApplicationUser_ApplicationUserId",
                table: "ApplicationUserPassword",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
