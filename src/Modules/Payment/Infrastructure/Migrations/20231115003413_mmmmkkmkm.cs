using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mmmmkkmkm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "payment",
                table: "wallets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "payment",
                table: "wallets");
        }
    }
}
