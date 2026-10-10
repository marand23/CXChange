using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class kkjnjn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_wallets_currencies_currency_temp_id",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropIndex(
                name: "ix_wallets_symbol",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropPrimaryKey(
                name: "pk_currencies",
                schema: "payment",
                table: "currencies");

            migrationBuilder.DropColumn(
                name: "symbol",
                schema: "payment",
                table: "wallets");

            migrationBuilder.RenameTable(
                name: "currencies",
                schema: "payment",
                newName: "currency_config",
                newSchema: "payment");

            migrationBuilder.RenameColumn(
                name: "type",
                schema: "payment",
                table: "currency_config",
                newName: "currency_type");

            migrationBuilder.AddColumn<int>(
                name: "currency_id",
                schema: "payment",
                table: "wallets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id",
                schema: "payment",
                table: "currency_config",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "deposit_enable",
                schema: "payment",
                table: "currency_config",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "withdraw_enable",
                schema: "payment",
                table: "currency_config",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "pk_currency_config",
                schema: "payment",
                table: "currency_config",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_wallets_currency_id",
                schema: "payment",
                table: "wallets",
                column: "currency_id");

            migrationBuilder.AddForeignKey(
                name: "fk_wallets_currency_config_currency_config_id",
                schema: "payment",
                table: "wallets",
                column: "currency_id",
                principalSchema: "payment",
                principalTable: "currency_config",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_wallets_currency_config_currency_config_id",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropIndex(
                name: "ix_wallets_currency_id",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropPrimaryKey(
                name: "pk_currency_config",
                schema: "payment",
                table: "currency_config");

            migrationBuilder.DropColumn(
                name: "currency_id",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "payment",
                table: "currency_config");

            migrationBuilder.DropColumn(
                name: "deposit_enable",
                schema: "payment",
                table: "currency_config");

            migrationBuilder.DropColumn(
                name: "withdraw_enable",
                schema: "payment",
                table: "currency_config");

            migrationBuilder.RenameTable(
                name: "currency_config",
                schema: "payment",
                newName: "currencies",
                newSchema: "payment");

            migrationBuilder.RenameColumn(
                name: "currency_type",
                schema: "payment",
                table: "currencies",
                newName: "type");

            migrationBuilder.AddColumn<string>(
                name: "symbol",
                schema: "payment",
                table: "wallets",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_currencies",
                schema: "payment",
                table: "currencies",
                column: "symbol");

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
        }
    }
}
