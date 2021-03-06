using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skipass.Database
{
    public partial class RemovedTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceCodes",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "Keys",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "PersistedGrants",
                schema: "skipass");

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "533333",
                column: "ValidTo",
                value: new DateTime(2022, 1, 27, 18, 10, 0, 992, DateTimeKind.Utc).AddTicks(7268));

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "792922",
                column: "ValidTo",
                value: new DateTime(2022, 1, 27, 17, 55, 0, 992, DateTimeKind.Utc).AddTicks(7266));

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "921212",
                column: "ValidTo",
                value: new DateTime(2022, 1, 27, 17, 55, 0, 992, DateTimeKind.Utc).AddTicks(7254));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                schema: "skipass",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ClientId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Data = table.Column<string>(type: "character varying(50000)", maxLength: 50000, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DeviceCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SessionId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SubjectId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                schema: "skipass",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Algorithm = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false),
                    DataProtected = table.Column<bool>(type: "boolean", nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "boolean", nullable: false),
                    Use = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                schema: "skipass",
                columns: table => new
                {
                    Key = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ClientId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ConsumedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Data = table.Column<string>(type: "character varying(50000)", maxLength: 50000, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SessionId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SubjectId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "533333",
                column: "ValidTo",
                value: new DateTime(2022, 1, 27, 17, 58, 25, 271, DateTimeKind.Utc).AddTicks(3572));

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "792922",
                column: "ValidTo",
                value: new DateTime(2022, 1, 27, 17, 43, 25, 271, DateTimeKind.Utc).AddTicks(3570));

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "921212",
                column: "ValidTo",
                value: new DateTime(2022, 1, 27, 17, 43, 25, 271, DateTimeKind.Utc).AddTicks(3555));

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                schema: "skipass",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                schema: "skipass",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_Use",
                schema: "skipass",
                table: "Keys",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                schema: "skipass",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                schema: "skipass",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                schema: "skipass",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                schema: "skipass",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });
        }
    }
}
