using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Skipass.Database
{
    public partial class AddedCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceListItem",
                schema: "skipass");

            migrationBuilder.AddColumn<int>(
                name: "OwnerIdentifier",
                schema: "skipass",
                table: "Gates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "skipass",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "skipass",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                schema: "skipass",
                table: "AspNetUserLogins",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "skipass",
                table: "AspNetUserLogins",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "skipass",
                columns: table => new
                {
                    Identifier = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "skipass",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    TimeAdded = table.Column<TimeSpan>(type: "interval", nullable: true),
                    PassagesAdded = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CardIdentifier = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Payments_Cards_CardIdentifier",
                        column: x => x.CardIdentifier,
                        principalSchema: "skipass",
                        principalTable: "Cards",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceListItems",
                schema: "skipass",
                columns: table => new
                {
                    Identifier = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListItems", x => x.Identifier);
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

            migrationBuilder.InsertData(
                schema: "skipass",
                table: "Companies",
                columns: new[] { "Identifier", "Name" },
                values: new object[,]
                {
                    { 1, "Mountain Resort Hotel" },
                    { 2, "Powder Lodge" },
                    { 3, "Ski Refuge" },
                    { 4, "The Elite Ski" }
                });

            migrationBuilder.InsertData(
                schema: "skipass",
                table: "PriceListItems",
                columns: new[] { "Identifier", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "2h", 20.0 },
                    { 2, "3h", 40.0 },
                    { 3, "4h", 60.0 },
                    { 4, "1d", 80.0 },
                    { 5, "2d", 100.0 },
                    { 6, "3d", 120.0 },
                    { 7, "4d", 140.0 },
                    { 8, "5d", 160.0 },
                    { 9, "6d", 180.0 },
                    { 10, "7d", 200.0 }
                });

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Gates",
                keyColumn: "Identifier",
                keyValue: "T9999",
                column: "OwnerIdentifier",
                value: 2);

            migrationBuilder.InsertData(
                schema: "skipass",
                table: "Gates",
                columns: new[] { "Identifier", "OwnerIdentifier" },
                values: new object[] { "T1234", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Gates_OwnerIdentifier",
                schema: "skipass",
                table: "Gates",
                column: "OwnerIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CardIdentifier",
                schema: "skipass",
                table: "Payments",
                column: "CardIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Gates_Companies_OwnerIdentifier",
                schema: "skipass",
                table: "Gates",
                column: "OwnerIdentifier",
                principalSchema: "skipass",
                principalTable: "Companies",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gates_Companies_OwnerIdentifier",
                schema: "skipass",
                table: "Gates");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "PriceListItems",
                schema: "skipass");

            migrationBuilder.DropIndex(
                name: "IX_Gates_OwnerIdentifier",
                schema: "skipass",
                table: "Gates");

            migrationBuilder.DeleteData(
                schema: "skipass",
                table: "Gates",
                keyColumn: "Identifier",
                keyValue: "T1234");

            migrationBuilder.DropColumn(
                name: "OwnerIdentifier",
                schema: "skipass",
                table: "Gates");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "skipass",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "skipass",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                schema: "skipass",
                table: "AspNetUserLogins",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "skipass",
                table: "AspNetUserLogins",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "PriceListItem",
                schema: "skipass",
                columns: table => new
                {
                    Identifier = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    PriceListItemName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListItem", x => x.Identifier);
                });

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "533333",
                column: "ValidTo",
                value: new DateTime(2022, 1, 24, 18, 42, 3, 870, DateTimeKind.Utc).AddTicks(3044));

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "792922",
                column: "ValidTo",
                value: new DateTime(2022, 1, 24, 18, 27, 3, 870, DateTimeKind.Utc).AddTicks(3042));

            migrationBuilder.UpdateData(
                schema: "skipass",
                table: "Cards",
                keyColumn: "Identifier",
                keyValue: "921212",
                column: "ValidTo",
                value: new DateTime(2022, 1, 24, 18, 27, 3, 870, DateTimeKind.Utc).AddTicks(3030));

            migrationBuilder.InsertData(
                schema: "skipass",
                table: "PriceListItem",
                columns: new[] { "Identifier", "Price", "PriceListItemName" },
                values: new object[,]
                {
                    { 1, 20, "2h" },
                    { 2, 40, "3h" },
                    { 3, 60, "4h" },
                    { 4, 80, "1d" },
                    { 5, 100, "2d" },
                    { 6, 120, "3d" },
                    { 7, 140, "4d" },
                    { 8, 160, "5d" },
                    { 9, 180, "6d" },
                    { 10, 200, "7d" }
                });
        }
    }
}
