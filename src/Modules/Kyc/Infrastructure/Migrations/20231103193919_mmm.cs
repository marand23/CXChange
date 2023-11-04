using System;
using CXChange.Modules.Kyc.Domain.KycRequest.Enums;
using CXChange.Modules.Kyc.Domain.User.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Kyc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mmm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "kyc");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:kyc.kyc_request_status", "active,stale")
                .Annotation("Npgsql:Enum:kyc.kyc_request_storage_type", "google_drive,drop_box,custom_s3")
                .Annotation("Npgsql:Enum:kyc.kyc_status", "unverified,in_progress,verified,rejected,denied");

            migrationBuilder.CreateTable(
                name: "kyc_requests",
                schema: "kyc",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    sur_name = table.Column<string>(type: "text", nullable: false),
                    national_id = table.Column<string>(type: "text", nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    kyc_photo_id = table.Column<string>(type: "text", nullable: false),
                    kyc_request_status = table.Column<KycRequestStatus>(type: "kyc.kyc_request_status", nullable: false),
                    storage_type = table.Column<StorageTypeEnum>(type: "kyc.kyc_request_storage_type", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_kyc_requests", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "kyc",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    sur_name = table.Column<string>(type: "text", nullable: true),
                    nationl_id = table.Column<string>(type: "text", nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    kyc_status = table.Column<KycStatus>(type: "kyc.kyc_status", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_nationl_id",
                schema: "kyc",
                table: "users",
                column: "nationl_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kyc_requests",
                schema: "kyc");

            migrationBuilder.DropTable(
                name: "users",
                schema: "kyc");
        }
    }
}
