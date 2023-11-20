using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class hbhbhbh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_wallets_currencies_balance_currency_symbol",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropForeignKey(
                name: "fk_wallets_currencies_reserved_balance_currency_symbol",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropIndex(
                name: "ix_wallets_balance_currency_symbol",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropIndex(
                name: "ix_wallets_reserved_balance_currency_symbol",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "balance_currency_symbol",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "reserved_balance_currency_symbol",
                schema: "payment",
                table: "wallets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "balance_currency_symbol",
                schema: "payment",
                table: "wallets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "reserved_balance_currency_symbol",
                schema: "payment",
                table: "wallets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_wallets_balance_currency_symbol",
                schema: "payment",
                table: "wallets",
                column: "balance_currency_symbol");

            migrationBuilder.CreateIndex(
                name: "ix_wallets_reserved_balance_currency_symbol",
                schema: "payment",
                table: "wallets",
                column: "reserved_balance_currency_symbol");

            migrationBuilder.AddForeignKey(
                name: "fk_wallets_currencies_balance_currency_symbol",
                schema: "payment",
                table: "wallets",
                column: "balance_currency_symbol",
                principalSchema: "payment",
                principalTable: "currencies",
                principalColumn: "symbol",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_wallets_currencies_reserved_balance_currency_symbol",
                schema: "payment",
                table: "wallets",
                column: "reserved_balance_currency_symbol",
                principalSchema: "payment",
                principalTable: "currencies",
                principalColumn: "symbol",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
