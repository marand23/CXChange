using System;
using System.Collections.Generic;
using CXChange.Modules.Payment.Domain.BankCard.Enums;
using CXChange.Modules.Payment.Domain.Shaba.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nnnnnnnnnnnn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deposit_accounts",
                schema: "payment",
                table: "wallets");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .Annotation("Npgsql:Enum:payment.shaba_status", "un_verified,waiting_admin_verification,verified,rejected")
                .OldAnnotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .OldAnnotation("Npgsql:Enum:payment.shaba_status", "un_verified,verified");

            migrationBuilder.CreateTable(
                name: "bank_cards",
                schema: "payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    status = table.Column<BankCardStatus>(type: "payment.bankcard_status", nullable: false),
                    bank_name = table.Column<string>(type: "text", nullable: false),
                    card_number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bank_cards", x => x.id);
                    table.ForeignKey(
                        name: "fk_bank_cards_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "payment",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shaba",
                schema: "payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<ShabaStatus>(type: "payment.shaba_status", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    bank_name = table.Column<string>(type: "text", nullable: false),
                    shaba_number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shaba", x => x.id);
                    table.ForeignKey(
                        name: "fk_shaba_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "payment",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_bank_cards_user_id",
                schema: "payment",
                table: "bank_cards",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_shaba_user_id",
                schema: "payment",
                table: "shaba",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bank_cards",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "shaba",
                schema: "payment");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .Annotation("Npgsql:Enum:payment.shaba_status", "un_verified,verified")
                .OldAnnotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .OldAnnotation("Npgsql:Enum:payment.shaba_status", "un_verified,waiting_admin_verification,verified,rejected");

            migrationBuilder.AddColumn<List<string>>(
                name: "deposit_accounts",
                schema: "payment",
                table: "wallets",
                type: "text[]",
                nullable: false);
        }
    }
}
