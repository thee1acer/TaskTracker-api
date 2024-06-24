using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedTaskEntityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedTo = table.Column<int>(type: "int", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailedDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskPriority = table.Column<int>(type: "int", nullable: false),
                    TaskStoryPoints = table.Column<int>(type: "int", nullable: false),
                    TaskStoryEffort = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskBlockerEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalTaskId = table.Column<int>(type: "int", nullable: false),
                    BlockerReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskBlockerEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskBlockerEntity_TaskEntity_OriginalTaskId",
                        column: x => x.OriginalTaskId,
                        principalTable: "TaskEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskBlockerEntity_OriginalTaskId",
                table: "TaskBlockerEntity",
                column: "OriginalTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskBlockerEntity");

            migrationBuilder.DropTable(
                name: "TaskEntity");
        }
    }
}
