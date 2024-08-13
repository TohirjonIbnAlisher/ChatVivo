using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enitities.Migrations
{
    /// <inheritdoc />
    public partial class update_user_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_moderator",
                schema: "auth",
                table: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_moderator",
                schema: "auth",
                table: "user",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
