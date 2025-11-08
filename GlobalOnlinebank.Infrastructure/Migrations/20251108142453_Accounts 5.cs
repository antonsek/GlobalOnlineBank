using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlobalOnlinebank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Accounts5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contragents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RuName = table.Column<string>(type: "text", nullable: true),
                    KzName = table.Column<string>(type: "text", nullable: true),
                    EnName = table.Column<string>(type: "text", nullable: true),
                    Bin = table.Column<string>(type: "text", nullable: true),
                    IsNew = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ContragentId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contragents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contragents_Contragents_ContragentId",
                        column: x => x.ContragentId,
                        principalTable: "Contragents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContragentID = table.Column<long>(type: "bigint", nullable: false),
                    AccountNumber = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    AccountType = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Contragents_ContragentID",
                        column: x => x.ContragentID,
                        principalTable: "Contragents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Contragents",
                columns: new[] { "Id", "Bin", "ContragentId", "CreatedDate", "EnName", "IsActive", "IsNew", "KzName", "RuName", "UpdatedDate" },
                values: new object[,]
                {
                    { -2L, "961545123251", null, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "LLP Rahat", true, true, "ТОО Рахат", "ТОО Рахат", null },
                    { -1L, "961545123222", null, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "LLP Kolbasa", true, true, "ТОО Колбаса", "ТОО Колбаса", null }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "AccountType", "Balance", "ContragentID", "CreatedDate", "Currency", "IsActive", "UpdatedDate" },
                values: new object[,]
                {
                    { -3L, "KZ1000000003", 1, 20000m, -1L, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, null },
                    { -2L, "KZ1000000002", 2, 500m, -1L, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "KZT", true, null },
                    { -1L, "KZ1000000001", 1, 10000m, -1L, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "KZT", true, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountNumber",
                table: "Accounts",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ContragentID",
                table: "Accounts",
                column: "ContragentID");

            migrationBuilder.CreateIndex(
                name: "IX_Contragents_ContragentId",
                table: "Contragents",
                column: "ContragentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Contragents");
        }
    }
}
