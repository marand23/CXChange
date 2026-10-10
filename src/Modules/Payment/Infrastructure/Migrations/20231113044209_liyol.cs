using CXChange.Modules.Payment.Domain.BankCard.Enums;
using CXChange.Modules.Payment.Domain.Shaba.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class liyol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "payment");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .Annotation("Npgsql:Enum:payment.shaba_status", "un_verified,verified");

            migrationBuilder.CreateTable(
                name: "currencies",
                schema: "payment",
                columns: table => new
                {
                    symbol = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    precision = table.Column<decimal>(type: "numeric", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currencies", x => x.symbol);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bank_cards",
                schema: "payment",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    card_number = table.Column<string>(type: "text", nullable: false),
                    bank_name = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<BankCardStatus>(type: "payment.bankcard_status", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bank_cards", x => new { x.user_id, x.id });
                    table.ForeignKey(
                        name: "fk_bank_cards_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "payment",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shabas",
                schema: "payment",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    shaba_number = table.Column<string>(type: "text", nullable: false),
                    bank_name = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<ShabaStatus>(type: "payment.shaba_status", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shabas", x => new { x.user_id, x.id });
                    table.ForeignKey(
                        name: "fk_shabas_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "payment",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wallets",
                schema: "payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false),
                    reserved_balance = table.Column<decimal>(type: "numeric", nullable: false),
                    nima = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_wallets", x => x.id);
                    table.ForeignKey(
                        name: "fk_wallets_currencies_currency_symbol",
                        column: x => x.nima,
                        principalSchema: "payment",
                        principalTable: "currencies",
                        principalColumn: "symbol");
                    table.ForeignKey(
                        name: "fk_wallets_users_user_id1",
                        column: x => x.user_id,
                        principalSchema: "payment",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_wallets_nima",
                schema: "payment",
                table: "wallets",
                column: "nima");

            migrationBuilder.CreateIndex(
                name: "ix_wallets_user_id",
                schema: "payment",
                table: "wallets",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bank_cards",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "shabas",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "wallets",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "currencies",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "users",
                schema: "payment");
        }
    }
}
