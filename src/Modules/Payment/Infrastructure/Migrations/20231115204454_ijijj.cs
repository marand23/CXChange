using CXChange.Modules.Payment.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ijijj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<IList<DepositAddress>>(
                name: "deposit_accounts",
                schema: "payment",
                table: "wallets",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(IList<DepositAddress>),
                oldType: "jsonb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<IList<DepositAddress>>(
                name: "deposit_accounts",
                schema: "payment",
                table: "wallets",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(IList<DepositAddress>),
                oldType: "jsonb",
                oldNullable: true);
        }
    }
}
