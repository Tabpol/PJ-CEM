using Microsoft.EntityFrameworkCore.Migrations;

namespace Nestle_service_api.Migrations
{
    public partial class N004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "number",
                table: "tb_logs_outbound",
                newName: "number_of_repeat");

            migrationBuilder.RenameColumn(
                name: "number",
                table: "tb_logs_inbound",
                newName: "number_of_repeat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "number_of_repeat",
                table: "tb_logs_outbound",
                newName: "number");

            migrationBuilder.RenameColumn(
                name: "number_of_repeat",
                table: "tb_logs_inbound",
                newName: "number");
        }
    }
}
