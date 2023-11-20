using System;
using CXChange.Modules.Payment.Domain.Currency.ValueObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class jnjnuyy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_wallets_users_user_id1",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "currency",
                schema: "payment",
                table: "wallets");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "payment",
                table: "wallets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "symbol",
                schema: "payment",
                table: "wallets",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_wallets_symbol",
                schema: "payment",
                table: "wallets",
                column: "symbol");

            migrationBuilder.AddForeignKey(
                name: "fk_wallets_currencies_currency_temp_id",
                schema: "payment",
                table: "wallets",
                column: "symbol",
                principalSchema: "payment",
                principalTable: "currencies",
                principalColumn: "symbol");

            migrationBuilder.AddForeignKey(
                name: "fk_wallets_users_user_id",
                schema: "payment",
                table: "wallets",
                column: "user_id",
                principalSchema: "payment",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_wallets_currencies_currency_temp_id",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropForeignKey(
                name: "fk_wallets_users_user_id",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropIndex(
                name: "ix_wallets_symbol",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "symbol",
                schema: "payment",
                table: "wallets");

            migrationBuilder.AddColumn<Currency>(
                name: "currency",
                schema: "payment",
                table: "wallets",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "fk_wallets_users_user_id1",
                schema: "payment",
                table: "wallets",
                column: "user_id",
                principalSchema: "payment",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
