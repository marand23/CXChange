using System;
using System.Collections.Generic;
using CXChange.Modules.Payment.Domain.BankCard.Enums;
using CXChange.Modules.Payment.Domain.Shaba.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class knknkn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bank_cards",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "shabas",
                schema: "payment");

            migrationBuilder.DropColumn(
                name: "deposit_accounts_capacity",
                schema: "payment",
                table: "wallets");

            migrationBuilder.AddColumn<List<string>>(
                name: "deposit_accounts",
                schema: "payment",
                table: "wallets",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "bank_cards",
                schema: "payment",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bank_name = table.Column<string>(type: "text", nullable: false),
                    card_number = table.Column<string>(type: "text", nullable: false),
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
                    bank_name = table.Column<string>(type: "text", nullable: false),
                    shaba_number = table.Column<string>(type: "text", nullable: false),
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
        }
    }
}
