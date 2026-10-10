using CXChange.Modules.Payment.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class njnjnjnj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deposit_accounts",
                schema: "payment",
                table: "wallets");

            migrationBuilder.AddColumn<int>(
                name: "deposit_accounts_capacity",
                schema: "payment",
                table: "wallets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deposit_accounts_capacity",
                schema: "payment",
                table: "wallets");

            migrationBuilder.AddColumn<IList<DepositAddress>>(
                name: "deposit_accounts",
                schema: "payment",
                table: "wallets",
                type: "jsonb",
                nullable: true);
        }
    }
}
