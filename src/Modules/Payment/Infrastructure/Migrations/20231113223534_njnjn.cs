using CXChange.Modules.Payment.Domain.Currency.ValueObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class njnjn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_wallets_currencies_currency_symbol",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropIndex(
                name: "ix_wallets_symbol",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currency",
                schema: "payment",
                table: "wallets");

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
                name: "fk_wallets_currencies_currency_symbol",
                schema: "payment",
                table: "wallets",
                column: "symbol",
                principalSchema: "payment",
                principalTable: "currencies",
                principalColumn: "symbol");
        }
    }
}
