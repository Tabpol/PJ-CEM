using Microsoft.EntityFrameworkCore.Migrations;

namespace Nestle_service_api.Migrations
{
    public partial class N003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "case_id",
                table: "tb_outbound_second_call",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "case_id",
                table: "tb_outbound_frist_call",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "case_id",
                table: "tb_outbound_second_call");

            migrationBuilder.DropColumn(
                name: "case_id",
                table: "tb_outbound_frist_call");
        }
    }
}
