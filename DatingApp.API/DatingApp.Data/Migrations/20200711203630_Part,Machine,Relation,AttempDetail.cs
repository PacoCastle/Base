using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.Data.Migrations
{
    public partial class PartMachineRelationAttempDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttemptDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachinePartRelationId = table.Column<int>(nullable: false),
                    AnguloLH = table.Column<string>(nullable: true),
                    MasaLH = table.Column<string>(nullable: true),
                    AnguloCL = table.Column<string>(nullable: true),
                    MasaCL = table.Column<string>(nullable: true),
                    AnguloRH = table.Column<string>(nullable: true),
                    MasaRH = table.Column<string>(nullable: true),
                    IsAccepted = table.Column<bool>(nullable: false),
                    Attempt = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttemptDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachinePartRelation",
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
                    table.PrimaryKey("PK_MachinePartRelation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Attempts = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttemptDetail");

            migrationBuilder.DropTable(
                name: "MachineModel");

            migrationBuilder.DropTable(
                name: "MachinePartRelation");

            migrationBuilder.DropTable(
                name: "PartModel");
        }
    }
}
