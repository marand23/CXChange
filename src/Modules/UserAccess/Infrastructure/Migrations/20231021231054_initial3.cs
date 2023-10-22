using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.UserAccess.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "address",
                schema: "user_access",
                table: "asp_net_users",
                type: "ipaddress",
                nullable: false,
                oldClrType: typeof(IPAddress),
                oldType: "inet");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<IPAddress>(
                name: "address",
                schema: "user_access",
                table: "asp_net_users",
                type: "inet",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "ipaddress");
        }
    }
}
