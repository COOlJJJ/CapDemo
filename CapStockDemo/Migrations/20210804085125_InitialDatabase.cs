using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CapStockDemo.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_STOCK",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(128)", nullable: false),
                    ProductNo = table.Column<string>(nullable: true),
                    STOCK_COUNT = table.Column<int>(nullable: false),
                    UPDATE_DATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STOCK", x => x.ID);
                },
                comment: "库存表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_STOCK");
        }
    }
}
