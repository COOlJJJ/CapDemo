using Microsoft.EntityFrameworkCore.Migrations;

namespace CapOderDemo.Migrations
{
    public partial class ModifyDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PRODUCT_NO",
                table: "TB_ORDER",
                type: "varchar(128)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PRODUCT_NO",
                table: "TB_ORDER");
        }
    }
}
