using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TASK_4_CSHARP.Migrations
{
    /// <inheritdoc />
    public partial class IsVerifiedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_verified",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_verified",
                table: "users");
        }
    }
}
