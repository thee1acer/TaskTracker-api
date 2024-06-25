using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTaskEntityInTaskBlocker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskBlockerEntity_TaskEntity_OriginalTaskId",
                table: "TaskBlockerEntity");

            migrationBuilder.DropIndex(
                name: "IX_TaskBlockerEntity_OriginalTaskId",
                table: "TaskBlockerEntity");

            migrationBuilder.AddColumn<int>(
                name: "TaskEntityId",
                table: "TaskBlockerEntity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskBlockerEntity_TaskEntityId",
                table: "TaskBlockerEntity",
                column: "TaskEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskBlockerEntity_TaskEntity_TaskEntityId",
                table: "TaskBlockerEntity",
                column: "TaskEntityId",
                principalTable: "TaskEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskBlockerEntity_TaskEntity_TaskEntityId",
                table: "TaskBlockerEntity");

            migrationBuilder.DropIndex(
                name: "IX_TaskBlockerEntity_TaskEntityId",
                table: "TaskBlockerEntity");

            migrationBuilder.DropColumn(
                name: "TaskEntityId",
                table: "TaskBlockerEntity");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBlockerEntity_OriginalTaskId",
                table: "TaskBlockerEntity",
                column: "OriginalTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskBlockerEntity_TaskEntity_OriginalTaskId",
                table: "TaskBlockerEntity",
                column: "OriginalTaskId",
                principalTable: "TaskEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
