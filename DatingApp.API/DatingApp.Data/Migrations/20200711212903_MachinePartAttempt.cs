using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.Data.Migrations
{
    public partial class MachinePartAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachinePartRelation");

            migrationBuilder.CreateTable(
                name: "MachinePartAttempt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineModelId = table.Column<int>(nullable: false),
                    PartModelId = table.Column<int>(nullable: false),
                    InternalSequence = table.Column<string>(nullable: true),
                    DefaultAttempts = table.Column<int>(nullable: false),
                    AvailableAttempts = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachinePartAttempt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachinePartAttempt");

            migrationBuilder.CreateTable(
                name: "MachinePartRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvailableAttempts = table.Column<int>(type: "int", nullable: false),
                    DefaultAttempts = table.Column<int>(type: "int", nullable: false),
                    InternalSequence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineModelId = table.Column<int>(type: "int", nullable: false),
                    PartModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachinePartRelation", x => x.Id);
                });
        }
    }
}
