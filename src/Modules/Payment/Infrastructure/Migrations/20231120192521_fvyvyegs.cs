using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fvyvyegs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "balance_currency_name",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "balance_currency_precision",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "balance_currency_symbol",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "balance_currency_type",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "reserved_balance_currency_name",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "reserved_balance_currency_precision",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "reserved_balance_currency_symbol",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "reserved_balance_currency_type",
                schema: "payment",
                table: "wallets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "balance_currency_name",
                schema: "payment",
                table: "wallets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "balance_currency_precision",
                schema: "payment",
                table: "wallets",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "balance_currency_symbol",
                schema: "payment",
                table: "wallets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "balance_currency_type",
                schema: "payment",
                table: "wallets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "reserved_balance_currency_name",
                schema: "payment",
                table: "wallets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "reserved_balance_currency_precision",
                schema: "payment",
                table: "wallets",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "reserved_balance_currency_symbol",
                schema: "payment",
                table: "wallets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "reserved_balance_currency_type",
                schema: "payment",
                table: "wallets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
