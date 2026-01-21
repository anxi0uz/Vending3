using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    MinimalStock = table.Column<long>(type: "bigint", nullable: false),
                    AvgDailySales = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fio = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeApparatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Place = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    SummaryIncome = table.Column<decimal>(type: "numeric", nullable: false),
                    SerialNumber = table.Column<Guid>(type: "uuid", nullable: false),
                    InventoryNumber = table.Column<Guid>(type: "uuid", nullable: false),
                    FirmName = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateOnly>(type: "date", nullable: true),
                    ManufacturedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DateUpdated = table.Column<DateOnly>(type: "date", nullable: true),
                    LastCheckDate = table.Column<DateOnly>(type: "date", nullable: true),
                    NextCheckInterval = table.Column<int>(type: "integer", nullable: false),
                    Resource = table.Column<long>(type: "bigint", nullable: false),
                    NextRepairDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RepairTime = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CountryOfManufacturer = table.Column<string>(type: "text", nullable: false),
                    InventarizationTime = table.Column<DateOnly>(type: "date", nullable: true),
                    CheckedByUserId = table.Column<int>(type: "integer", nullable: true),
                    NextCheckDate = table.Column<DateOnly>(type: "date", nullable: true, computedColumnSql: "CASE WHEN \"LastCheckDate\" IS NULL THEN NULL ELSE \"LastCheckDate\" + (\"NextCheckInterval\" * INTERVAL '1 month') END", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeApparatuses", x => x.Id);
                    table.CheckConstraint("CK_DateUpdated_Range", "\"DateUpdated\" IS NULL OR (\"DateUpdated\" >= \"DateCreated\" AND \"DateUpdated\" <= CURRENT_DATE)");
                    table.CheckConstraint("CK_InventarizationTime_Range", "\"InventarizationTime\" IS NULL OR (\"InventarizationTime\" >= \"DateCreated\" AND \"InventarizationTime\" <= CURRENT_DATE)");
                    table.CheckConstraint("CK_LastCheckDate_Range", "\"LastCheckDate\" IS NULL OR (\"LastCheckDate\" >= \"DateCreated\" AND \"LastCheckDate\" <= CURRENT_DATE)");
                    table.CheckConstraint("CK_NextCheckInterval_NonNegative", "\"NextCheckInterval\" >= 0");
                    table.CheckConstraint("CK_NextRepairDate_Future", "\"NextRepairDate\" IS NULL OR \"NextRepairDate\" > CURRENT_DATE");
                    table.CheckConstraint("CK_RepairTime_Range", "\"RepairTime\" BETWEEN 1 AND 20");
                    table.CheckConstraint("CK_Resource_Positive", "\"Resource\" > 0");
                    table.ForeignKey(
                        name: "FK_TradeApparatuses_Users_CheckedByUserId",
                        column: x => x.CheckedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApparatusId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PayType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.CheckConstraint("CK_TotalPrice_NonNegative", "\"TotalPrice\" >= 0");
                    table.ForeignKey(
                        name: "FK_Sales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_TradeApparatuses_ApparatusId",
                        column: x => x.ApparatusId,
                        principalTable: "TradeApparatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApparatusId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Problems = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_TradeApparatuses_ApparatusId",
                        column: x => x.ApparatusId,
                        principalTable: "TradeApparatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ApparatusId",
                table: "Sales",
                column: "ApparatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ApparatusId",
                table: "Services",
                column: "ApparatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId",
                table: "Services",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeApparatuses_CheckedByUserId",
                table: "TradeApparatuses",
                column: "CheckedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeApparatuses_SerialNumber",
                table: "TradeApparatuses",
                column: "SerialNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationLogs");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TradeApparatuses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
