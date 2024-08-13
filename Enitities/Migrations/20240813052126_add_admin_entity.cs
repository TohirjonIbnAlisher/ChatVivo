using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Enitities.Migrations
{
    /// <inheritdoc />
    public partial class add_admin_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat_member",
                schema: "chats");

            migrationBuilder.DropColumn(
                name: "email",
                schema: "auth",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "auth",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "fist_name",
                schema: "auth",
                table: "user",
                newName: "fio");

            migrationBuilder.AddColumn<int>(
                name: "status",
                schema: "chats",
                table: "chat",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                schema: "chats",
                table: "chat");

            migrationBuilder.RenameColumn(
                name: "fio",
                schema: "auth",
                table: "user",
                newName: "fist_name");

            migrationBuilder.AddColumn<string>(
                name: "email",
                schema: "auth",
                table: "user",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "auth",
                table: "user",
                type: "character varying(125)",
                maxLength: 125,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "chat_member",
                schema: "chats",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    chat_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_member", x => x.id);
                    table.ForeignKey(
                        name: "FK_chat_member_chat_chat_id",
                        column: x => x.chat_id,
                        principalSchema: "chats",
                        principalTable: "chat",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chat_member_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chat_member_chat_id",
                schema: "chats",
                table: "chat_member",
                column: "chat_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_member_user_id",
                schema: "chats",
                table: "chat_member",
                column: "user_id");
        }
    }
}
