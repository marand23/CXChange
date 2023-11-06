using CXChange.Modules.Kyc.Domain.User.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXChange.Modules.Kyc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fsdfsdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:kyc.kyc_land_line_status", "un_verified,verified")
                .Annotation("Npgsql:Enum:kyc.kyc_request_status", "active,stale")
                .Annotation("Npgsql:Enum:kyc.kyc_request_storage_type", "google_drive,drop_box,custom_s3")
                .Annotation("Npgsql:Enum:kyc.kyc_status", "unverified,in_progress,verified,rejected,denied")
                .OldAnnotation("Npgsql:Enum:kyc.kyc_request_status", "active,stale")
                .OldAnnotation("Npgsql:Enum:kyc.kyc_request_storage_type", "google_drive,drop_box,custom_s3")
                .OldAnnotation("Npgsql:Enum:kyc.kyc_status", "unverified,in_progress,verified,rejected,denied");

            migrationBuilder.AddColumn<string>(
                name: "land_line",
                schema: "kyc",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<LandLineStatus>(
                name: "land_line_status",
                schema: "kyc",
                table: "users",
                type: "kyc.kyc_land_line_status",
                nullable: false,
                defaultValue: LandLineStatus.UnVerified);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "land_line",
                schema: "kyc",
                table: "users");

            migrationBuilder.DropColumn(
                name: "land_line_status",
                schema: "kyc",
                table: "users");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:kyc.kyc_request_status", "active,stale")
                .Annotation("Npgsql:Enum:kyc.kyc_request_storage_type", "google_drive,drop_box,custom_s3")
                .Annotation("Npgsql:Enum:kyc.kyc_status", "unverified,in_progress,verified,rejected,denied")
                .OldAnnotation("Npgsql:Enum:kyc.kyc_land_line_status", "un_verified,verified")
                .OldAnnotation("Npgsql:Enum:kyc.kyc_request_status", "active,stale")
                .OldAnnotation("Npgsql:Enum:kyc.kyc_request_storage_type", "google_drive,drop_box,custom_s3")
                .OldAnnotation("Npgsql:Enum:kyc.kyc_status", "unverified,in_progress,verified,rejected,denied");
        }
    }
}
