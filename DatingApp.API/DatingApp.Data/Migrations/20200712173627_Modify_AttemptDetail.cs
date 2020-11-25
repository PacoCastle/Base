using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.Data.Migrations
{
    public partial class Modify_AttemptDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MachinePartRelationId",
                table: "AttemptDetail");

            migrationBuilder.AddColumn<int>(
                name: "MachinePartAttemptId",
                table: "AttemptDetail",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MachinePartAttemptId",
                table: "AttemptDetail");

            migrationBuilder.AddColumn<int>(
                name: "MachinePartRelationId",
                table: "AttemptDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
