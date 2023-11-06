using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Kyc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nnnnsafsf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                schema: "kyc",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<int>(
                name: "version",
                schema: "kyc",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                schema: "kyc",
                table: "kyc_requests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "kyc",
                table: "users");

            migrationBuilder.DropColumn(
                name: "version",
                schema: "kyc",
                table: "users");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "kyc",
                table: "kyc_requests");
        }
    }
}
