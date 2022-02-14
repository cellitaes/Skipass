using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Skipass.Database
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "skipass");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "skipass",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "skipass",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                schema: "skipass",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PassagesLeft = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                schema: "skipass",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Data = table.Column<string>(type: "character varying(50000)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Gates",
                schema: "skipass",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gates", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                schema: "skipass",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Use = table.Column<string>(type: "text", nullable: true),
                    Algorithm = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "boolean", nullable: false),
                    DataProtected = table.Column<bool>(type: "boolean", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false)
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
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Data = table.Column<string>(type: "character varying(50000)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "PriceListItem",
                schema: "skipass",
                columns: table => new
                {
                    Identifier = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PriceListItemName = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListItem", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "skipass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "skipass",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "skipass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "skipass",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "skipass",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "skipass",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "skipass",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "skipass",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "skipass",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "skipass",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "skipass",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passages",
                schema: "skipass",
                columns: table => new
                {
                    Identifier = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CardIdentifier = table.Column<string>(type: "text", nullable: false),
                    GateIdentifier = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passages", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Passages_Cards_CardIdentifier",
                        column: x => x.CardIdentifier,
                        principalSchema: "skipass",
                        principalTable: "Cards",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Passages_Gates_GateIdentifier",
                        column: x => x.GateIdentifier,
                        principalSchema: "skipass",
                        principalTable: "Gates",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "skipass",
                table: "Cards",
                columns: new[] { "Identifier", "PassagesLeft", "ValidTo" },
                values: new object[,]
                {
                    { "533333", 0, new DateTime(2022, 1, 24, 18, 42, 3, 870, DateTimeKind.Utc).AddTicks(3044) },
                    { "792922", 15, new DateTime(2022, 1, 24, 18, 27, 3, 870, DateTimeKind.Utc).AddTicks(3042) },
                    { "921212", 2, new DateTime(2022, 1, 24, 18, 27, 3, 870, DateTimeKind.Utc).AddTicks(3030) }
                });

            migrationBuilder.InsertData(
                schema: "skipass",
                table: "Gates",
                column: "Identifier",
                value: "T9999");

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "skipass",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "skipass",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "skipass",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "skipass",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "skipass",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "skipass",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "skipass",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

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
                name: "IX_Passages_CardIdentifier",
                schema: "skipass",
                table: "Passages",
                column: "CardIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Passages_GateIdentifier",
                schema: "skipass",
                table: "Passages",
                column: "GateIdentifier");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "DeviceCodes",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "Keys",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "Passages",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "PersistedGrants",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "PriceListItem",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "Cards",
                schema: "skipass");

            migrationBuilder.DropTable(
                name: "Gates",
                schema: "skipass");
        }
    }
}
