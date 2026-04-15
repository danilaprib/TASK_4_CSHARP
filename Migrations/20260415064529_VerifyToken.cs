using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TASK_4_CSHARP.Migrations
{
    /// <inheritdoc />
    public partial class VerifyToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "verification_token",
                table: "users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "verification_token",
                table: "users");
        }
    }
}
