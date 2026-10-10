using System;
using CXChange.Modules.Payment.Domain.Deposit.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class jnjnnjh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .Annotation("Npgsql:Enum:payment.deposit_status", "in_progress,failed,done")
                .Annotation("Npgsql:Enum:payment.shaba_status", "un_verified,waiting_admin_verification,verified,rejected")
                .OldAnnotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .OldAnnotation("Npgsql:Enum:payment.shaba_status", "un_verified,waiting_admin_verification,verified,rejected");

            migrationBuilder.AddColumn<bool>(
                name: "can_deposit",
                schema: "payment",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "can_withdraw",
                schema: "payment",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "deposits",
                schema: "payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    wallet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<DepositStatus>(type: "payment.deposit_status", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false, defaultValue: ""),
                    value = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    currency = table.Column<string>(type: "text", nullable: false),
                    precision = table.Column<decimal>(type: "numeric", nullable: false),
                    amount_currency_symbol = table.Column<string>(type: "text", nullable: false),
                    amount_currency_type = table.Column<int>(type: "integer", nullable: false),
                    track_id = table.Column<string>(type: "text", nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deposits", x => x.id);
                    table.ForeignKey(
                        name: "fk_deposits_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "payment",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_deposits_wallets_wallet_id",
                        column: x => x.wallet_id,
                        principalSchema: "payment",
                        principalTable: "wallets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_deposits_user_id",
                schema: "payment",
                table: "deposits",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_deposits_wallet_id",
                schema: "payment",
                table: "deposits",
                column: "wallet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deposits",
                schema: "payment");

            migrationBuilder.DropColumn(
                name: "can_deposit",
                schema: "payment",
                table: "users");

            migrationBuilder.DropColumn(
                name: "can_withdraw",
                schema: "payment",
                table: "users");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .Annotation("Npgsql:Enum:payment.shaba_status", "un_verified,waiting_admin_verification,verified,rejected")
                .OldAnnotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .OldAnnotation("Npgsql:Enum:payment.deposit_status", "in_progress,failed,done")
                .OldAnnotation("Npgsql:Enum:payment.shaba_status", "un_verified,waiting_admin_verification,verified,rejected");
        }
    }
}
