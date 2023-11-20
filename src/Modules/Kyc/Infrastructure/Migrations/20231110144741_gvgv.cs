using System;
using CXChange.Modules.Kyc.Domain.User.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Kyc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class gvgv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "kyc");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:kyc.kyc_land_line_status", "un_verified,verified")
                .Annotation("Npgsql:Enum:kyc.kyc_request_status", "active,stale")
                .Annotation("Npgsql:Enum:kyc.kyc_request_storage_type", "google_drive,drop_box,custom_s3")
                .Annotation("Npgsql:Enum:kyc.kyc_status", "unverified,in_progress,verified,rejected,denied");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "kyc",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    kyc_status = table.Column<KycStatus>(type: "kyc.kyc_status", nullable: false),
                    land_line = table.Column<string>(type: "text", nullable: true),
                    land_line_status = table.Column<LandLineStatus>(type: "kyc.kyc_land_line_status", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    name = table.Column<string>(type: "text", nullable: true),
                    sur_name = table.Column<string>(type: "text", nullable: true),
                    national_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_request",
                schema: "kyc",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    sur_name = table.Column<string>(type: "text", nullable: true),
                    national_id = table.Column<string>(type: "text", nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    kyc_photo_id = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    status = table.Column<KycRequestStatus>(type: "kyc.kyc_request_status", nullable: false),
                    storage_type = table.Column<StorageTypeEnum>(type: "kyc.kyc_request_storage_type", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_kyc_request", x => x.id);
                    table.ForeignKey(
                        name: "fk_kyc_request_users_id",
                        column: x => x.id,
                        principalSchema: "kyc",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_kyc_request_national_id",
                schema: "kyc",
                table: "kyc_request",
                column: "national_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_national_id",
                schema: "kyc",
                table: "users",
                column: "national_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kyc_request",
                schema: "kyc");

            migrationBuilder.DropTable(
                name: "users",
                schema: "kyc");
        }
    }
}
