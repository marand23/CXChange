using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bksnd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_wallets_currency_config_currency_config_id",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropPrimaryKey(
                name: "pk_currency_config",
                schema: "payment",
                table: "currency_config");

            migrationBuilder.RenameTable(
                name: "currency_config",
                schema: "payment",
                newName: "currency_configs",
                newSchema: "payment");

            migrationBuilder.AddPrimaryKey(
                name: "pk_currency_configs",
                schema: "payment",
                table: "currency_configs",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_wallets_currency_configs_currency_config_id",
                schema: "payment",
                table: "wallets",
                column: "currency_id",
                principalSchema: "payment",
                principalTable: "currency_configs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_wallets_currency_configs_currency_config_id",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropPrimaryKey(
                name: "pk_currency_configs",
                schema: "payment",
                table: "currency_configs");

            migrationBuilder.RenameTable(
                name: "currency_configs",
                schema: "payment",
                newName: "currency_config",
                newSchema: "payment");

            migrationBuilder.AddPrimaryKey(
                name: "pk_currency_config",
                schema: "payment",
                table: "currency_config",
                column: "id");

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
    }
}
