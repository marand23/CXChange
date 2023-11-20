using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class llmlll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount_currency_symbol",
                schema: "payment",
                table: "deposits");

            migrationBuilder.DropColumn(
                name: "amount_currency_type",
                schema: "payment",
                table: "deposits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "amount_currency_symbol",
                schema: "payment",
                table: "deposits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "amount_currency_type",
                schema: "payment",
                table: "deposits",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
