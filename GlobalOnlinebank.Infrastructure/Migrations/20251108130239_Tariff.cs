using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlobalOnlinebank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Tariff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Contragents",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MinPoints = table.Column<int>(type: "integer", nullable: false),
                    MaxPoints = table.Column<int>(type: "integer", nullable: false),
                    CommissionDiscountPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    RateImprovementPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    BonusPoints = table.Column<int>(type: "integer", nullable: false),
                    HasPriorityService = table.Column<bool>(type: "boolean", nullable: false),
                    HasPersonalManager = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tariffs",
                columns: new[] { "Id", "BonusPoints", "CommissionDiscountPercent", "CreatedDate", "Description", "HasPersonalManager", "HasPriorityService", "MaxPoints", "MinPoints", "Name", "RateImprovementPercent", "UpdatedDate" },
                values: new object[,]
                {
                    { -6L, 50000, 15m, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "15 000+ баллов — скидка 15%, бонус 50 000, полный пакет привилегий", true, true, 2147483647, 15000, "Privilege", 0.1m, null },
                    { -5L, 40000, 10m, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "10 000–15 000 баллов — скидка 10%, бонус 40 000, персональный менеджер", true, true, 14999, 10000, "Executive", 0.05m, null },
                    { -4L, 30000, 7m, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "8000–10000 баллов — скидка 7%, бонус 30 000, приоритетная обработка", false, true, 9999, 8000, "Advance", 0m, null },
                    { -3L, 20000, 5m, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "6000–8000 баллов — скидка 5%, бонус 20 000 баллов, улучшение курса +0.05%", false, false, 7999, 6000, "Prime PRO", 0.05m, null },
                    { -2L, 10000, 3m, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "3000–6000 баллов — скидка 3%, бонус 10 000 баллов", false, false, 5999, 3000, "Prime", 0m, null },
                    { -1L, 0, 0m, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "0–3000 баллов — стандартные условия", false, false, 2999, 0, "Base", 0m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Contragents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
