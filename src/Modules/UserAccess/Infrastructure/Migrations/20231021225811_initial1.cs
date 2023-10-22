using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.UserAccess.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "tags",
                schema: "user_access",
                table: "asp_net_users",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tags",
                schema: "user_access",
                table: "asp_net_users");
        }
    }
}
