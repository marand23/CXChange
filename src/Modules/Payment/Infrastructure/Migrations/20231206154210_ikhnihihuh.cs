using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ikhnihihuh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "banks",
                schema: "payment",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "Networks",
                schema: "payment",
                table: "currency_configs");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .Annotation("Npgsql:Enum:payment.deposit_status", "in_progress,failed,done")
                .Annotation("Npgsql:Enum:payment.shaba_status", "un_verified,waiting_admin_verification,verified,rejected")
                .Annotation("Npgsql:Enum:payment.withdraw_sattus", "confirmed_by_user,needs_manual_interversion,auditing,cancled,sent,completed,failed_needs_manual_interversion,failed")
                .OldAnnotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .OldAnnotation("Npgsql:Enum:payment.deposit_status", "in_progress,failed,done")
                .OldAnnotation("Npgsql:Enum:payment.shaba_status", "un_verified,waiting_admin_verification,verified,rejected");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .Annotation("Npgsql:Enum:payment.deposit_status", "in_progress,failed,done")
                .Annotation("Npgsql:Enum:payment.shaba_status", "un_verified,waiting_admin_verification,verified,rejected")
                .OldAnnotation("Npgsql:Enum:payment.bankcard_status", "un_verified,waiting_admin_verification,verified,rejected")
                .OldAnnotation("Npgsql:Enum:payment.deposit_status", "in_progress,failed,done")
                .OldAnnotation("Npgsql:Enum:payment.shaba_status", "un_verified,waiting_admin_verification,verified,rejected")
                .OldAnnotation("Npgsql:Enum:payment.withdraw_sattus", "confirmed_by_user,needs_manual_interversion,auditing,cancled,sent,completed,failed_needs_manual_interversion,failed");

            migrationBuilder.AddColumn<string>(
                name: "banks",
                schema: "payment",
                table: "wallets",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Networks",
                schema: "payment",
                table: "currency_configs",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }
    }
}
