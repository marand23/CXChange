using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class kgj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nima",
                schema: "payment",
                table: "wallets",
                newName: "symbol");

            migrationBuilder.RenameIndex(
                name: "ix_wallets_nima",
                schema: "payment",
                table: "wallets",
                newName: "ix_wallets_symbol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "symbol",
                schema: "payment",
                table: "wallets",
                newName: "nima");

            migrationBuilder.RenameIndex(
                name: "ix_wallets_symbol",
                schema: "payment",
                table: "wallets",
                newName: "ix_wallets_nima");
        }
    }
}
