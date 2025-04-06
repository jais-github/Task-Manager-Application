using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class AddTagSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagTaskItem_Tags_TagsId",
                table: "TagTaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TagTaskItem_Tasks_TasksId",
                table: "TagTaskItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagTaskItem",
                table: "TagTaskItem");

            migrationBuilder.RenameTable(
                name: "TagTaskItem",
                newName: "TaskTags");

            migrationBuilder.RenameIndex(
                name: "IX_TagTaskItem_TasksId",
                table: "TaskTags",
                newName: "IX_TaskTags_TasksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskTags",
                table: "TaskTags",
                columns: new[] { "TagsId", "TasksId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTags_Tags_TagsId",
                table: "TaskTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTags_Tasks_TasksId",
                table: "TaskTags",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTags_Tags_TagsId",
                table: "TaskTags");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTags_Tasks_TasksId",
                table: "TaskTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskTags",
                table: "TaskTags");

            migrationBuilder.RenameTable(
                name: "TaskTags",
                newName: "TagTaskItem");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTags_TasksId",
                table: "TagTaskItem",
                newName: "IX_TagTaskItem_TasksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagTaskItem",
                table: "TagTaskItem",
                columns: new[] { "TagsId", "TasksId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TagTaskItem_Tags_TagsId",
                table: "TagTaskItem",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagTaskItem_Tasks_TasksId",
                table: "TagTaskItem",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
