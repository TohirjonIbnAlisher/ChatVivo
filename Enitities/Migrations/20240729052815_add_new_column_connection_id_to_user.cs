using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enitities.Migrations
{
    /// <inheritdoc />
    public partial class add_new_column_connection_id_to_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "connection_id",
                schema: "auth",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_moderator",
                schema: "auth",
                table: "user",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "connection_id",
                schema: "auth",
                table: "user");

            migrationBuilder.DropColumn(
                name: "is_moderator",
                schema: "auth",
                table: "user");
        }
    }
}
