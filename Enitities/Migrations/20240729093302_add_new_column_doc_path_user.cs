using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enitities.Migrations
{
    /// <inheritdoc />
    public partial class add_new_column_doc_path_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "text",
                schema: "chats",
                table: "message",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "doc_path",
                schema: "chats",
                table: "message",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                schema: "chats",
                table: "chat",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_chat_user_id",
                schema: "chats",
                table: "chat",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_chat_user_user_id",
                schema: "chats",
                table: "chat",
                column: "user_id",
                principalSchema: "auth",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chat_user_user_id",
                schema: "chats",
                table: "chat");

            migrationBuilder.DropIndex(
                name: "IX_chat_user_id",
                schema: "chats",
                table: "chat");

            migrationBuilder.DropColumn(
                name: "doc_path",
                schema: "chats",
                table: "message");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "chats",
                table: "chat");

            migrationBuilder.AlterColumn<string>(
                name: "text",
                schema: "chats",
                table: "message",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
