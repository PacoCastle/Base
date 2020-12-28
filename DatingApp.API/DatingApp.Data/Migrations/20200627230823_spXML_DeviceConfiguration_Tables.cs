using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.Data.Migrations
{
    public partial class spXML_DeviceConfiguration_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerName = table.Column<string>(nullable: true),
                    Chanel = table.Column<string>(nullable: true),
                    Device = table.Column<string>(nullable: true),
                    TagName = table.Column<string>(nullable: true),
                    IsStart = table.Column<string>(nullable: true),
                    IsActive = table.Column<string>(nullable: true),
                    ColumnDataType = table.Column<string>(nullable: true),
                    ColumnNullable = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceConfiguration", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceConfiguration");
        }
    }
}
