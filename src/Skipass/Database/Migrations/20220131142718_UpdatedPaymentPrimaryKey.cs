using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Skipass.Database
{
    public partial class UpdatedPaymentPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifier",
                schema: "skipass",
                table: "Payments");

            migrationBuilder.AddColumn<long>(
                name: "Identifier",
                schema: "skipass",
                table: "Payments",
                type: "bigint",
                nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "533333",
                column: "ValidTo",
                value: new DateTime(2022, 1, 31, 14, 42, 18, 372, DateTimeKind.Utc).AddTicks(2139));

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "792922",
                column: "ValidTo",
                value: new DateTime(2022, 1, 31, 14, 27, 18, 372, DateTimeKind.Utc).AddTicks(2137));

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "921212",
                column: "ValidTo",
                value: new DateTime(2022, 1, 31, 14, 27, 18, 372, DateTimeKind.Utc).AddTicks(2124));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifier",
                schema: "skipass",
                table: "Payments");

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                schema: "skipass",
                table: "Payments",
                type: "text",
                nullable: false);

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "533333",
                column: "ValidTo",
                value: new DateTime(2022, 1, 27, 18, 36, 11, 640, DateTimeKind.Utc).AddTicks(3102));

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "792922",
                column: "ValidTo",
                value: new DateTime(2022, 1, 27, 18, 21, 11, 640, DateTimeKind.Utc).AddTicks(3100));

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "921212",
                column: "ValidTo",
                value: new DateTime(2022, 1, 27, 18, 21, 11, 640, DateTimeKind.Utc).AddTicks(3082));
        }
    }
}
