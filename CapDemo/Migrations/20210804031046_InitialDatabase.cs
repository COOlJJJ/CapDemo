using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CapOderDemo.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_ORDER",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(128)", nullable: false),
                    ORDER_NO = table.Column<string>(type: "varchar(128)", nullable: true),
                    PRODUCT_NAME = table.Column<string>(type: "varchar(128)", nullable: true),
                    COUNT = table.Column<int>(nullable: false),
                    CREATE_DATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ORDER", x => x.ID);
                },
                comment: "订单表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ORDER");
        }
    }
}
