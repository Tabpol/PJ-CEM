using Microsoft.EntityFrameworkCore.Migrations;

namespace Nestle_service_api.Migrations
{
    public partial class N005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "aqent_id",
                table: "tb_logs_outbound");

            migrationBuilder.DropColumn(
                name: "aqent_id",
                table: "tb_logs_inbound");

            migrationBuilder.AddColumn<string>(
                name: "aqent_name",
                table: "tb_logs_outbound",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "aqent_name",
                table: "tb_logs_inbound",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "aqent_name",
                table: "tb_logs_outbound");

            migrationBuilder.DropColumn(
                name: "aqent_name",
                table: "tb_logs_inbound");

            migrationBuilder.AddColumn<int>(
                name: "aqent_id",
                table: "tb_logs_outbound",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "aqent_id",
                table: "tb_logs_inbound",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
