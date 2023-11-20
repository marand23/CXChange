using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class jnjnjnnbv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_wallets_currency_id",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "payment",
                table: "wallets");

            migrationBuilder.AddColumn<int>(
                name: "version",
                schema: "payment",
                table: "wallets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_wallets_currency_id_user_id",
                schema: "payment",
                table: "wallets",
                columns: new[] { "currency_id", "user_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_wallets_currency_id_user_id",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "version",
                schema: "payment",
                table: "wallets");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "payment",
                table: "wallets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "ix_wallets_currency_id",
                schema: "payment",
                table: "wallets",
                column: "currency_id");
        }
    }
}
