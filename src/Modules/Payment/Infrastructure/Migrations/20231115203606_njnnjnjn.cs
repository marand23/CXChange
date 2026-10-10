using CXChange.Modules.Payment.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class njnnjnjn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<IList<DepositAddress>>(
                name: "deposit_accounts",
                schema: "payment",
                table: "wallets",
                type: "jsonb",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deposit_accounts",
                schema: "payment",
                table: "wallets");
        }
    }
}
