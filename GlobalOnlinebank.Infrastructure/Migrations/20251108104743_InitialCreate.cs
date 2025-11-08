using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlobalOnlinebank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contragents", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Contragents",
                columns: new[] { "Id", "Bin", "CreatedDate", "EnName", "IsActive", "IsNew", "KzName", "RuName", "UpdatedDate" },
                values: new object[,]
                {
                    { -2L, "961545123251", new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "LLP Rahat", true, true, "ТОО Рахат", "ТОО Рахат", null },
                    { -1L, "961545123222", new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "LLP Kolbasa", true, true, "ТОО Колбаса", "ТОО Колбаса", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contragents");
        }
    }
}
