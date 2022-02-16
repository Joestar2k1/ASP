using Microsoft.EntityFrameworkCore.Migrations;

namespace FshopASP.Migrations
{
    public partial class edit_invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeVoucher",
                table: "Invoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeVoucher",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
