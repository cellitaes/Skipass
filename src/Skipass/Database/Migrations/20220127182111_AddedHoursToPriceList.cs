using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skipass.Database
{
    public partial class AddedHoursToPriceList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Hours",
                schema: "skipass",
                table: "PriceListItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "PriceListItems",
                keyColumn: "Identifier",
                keyValue: 1,
                column: "Hours",
                value: 2);

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "PriceListItems",
                keyColumn: "Identifier",
                keyValue: 2,
                column: "Hours",
                value: 3);

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "PriceListItems",
                keyColumn: "Identifier",
                keyValue: 3,
                column: "Hours",
                value: 4);

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "PriceListItems",
                keyColumn: "Identifier",
                keyValue: 4,
                column: "Hours",
                value: 24);

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "PriceListItems",
                keyColumn: "Identifier",
                keyValue: 5,
                column: "Hours",
                value: 48);

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "PriceListItems",
                keyColumn: "Identifier",
                keyValue: 6,
                column: "Hours",
                value: 72);

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "PriceListItems",
                keyColumn: "Identifier",
                keyValue: 7,
                column: "Hours",
                value: 96);

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "PriceListItems",
                keyColumn: "Identifier",
                keyValue: 8,
                column: "Hours",
                value: 120);

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "PriceListItems",
                keyColumn: "Identifier",
                keyValue: 9,
                column: "Hours",
                value: 144);

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "PriceListItems",
                keyColumn: "Identifier",
                keyValue: 10,
                column: "Hours",
                value: 168);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours",
                schema: "skipass",
                table: "PriceListItems");

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
    }
}
