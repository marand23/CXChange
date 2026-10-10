using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nnnnnn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_shaba_users_user_id",
                schema: "payment",
                table: "shaba");

            migrationBuilder.DropPrimaryKey(
                name: "pk_shaba",
                schema: "payment",
                table: "shaba");

            migrationBuilder.RenameTable(
                name: "shaba",
                schema: "payment",
                newName: "shabas",
                newSchema: "payment");

            migrationBuilder.RenameIndex(
                name: "ix_shaba_user_id",
                schema: "payment",
                table: "shabas",
                newName: "ix_shabas_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_shabas",
                schema: "payment",
                table: "shabas",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_shabas_users_user_id",
                schema: "payment",
                table: "shabas",
                column: "user_id",
                principalSchema: "payment",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_shabas_users_user_id",
                schema: "payment",
                table: "shabas");

            migrationBuilder.DropPrimaryKey(
                name: "pk_shabas",
                schema: "payment",
                table: "shabas");

            migrationBuilder.RenameTable(
                name: "shabas",
                schema: "payment",
                newName: "shaba",
                newSchema: "payment");

            migrationBuilder.RenameIndex(
                name: "ix_shabas_user_id",
                schema: "payment",
                table: "shaba",
                newName: "ix_shaba_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_shaba",
                schema: "payment",
                table: "shaba",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_shaba_users_user_id",
                schema: "payment",
                table: "shaba",
                column: "user_id",
                principalSchema: "payment",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
