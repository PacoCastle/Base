using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.Data.Migrations
{
    public partial class Modify_Parts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiametroTubo",
                table: "PartModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoPlanos",
                table: "PartModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RPM",
                table: "PartModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiametroTubo",
                table: "PartModel");

            migrationBuilder.DropColumn(
                name: "NoPlanos",
                table: "PartModel");

            migrationBuilder.DropColumn(
                name: "RPM",
                table: "PartModel");
        }
    }
}
