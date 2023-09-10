using Microsoft.EntityFrameworkCore.Migrations;

namespace WebLab2.DAL.Migrations
{
    public partial class EntitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransportGroups",
                columns: table => new
                {
                    TransportGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportGroups", x => x.TransportGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    TransportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    TransportGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.TransportId);
                    table.ForeignKey(
                        name: "FK_Transports_TransportGroups_TransportGroupId",
                        column: x => x.TransportGroupId,
                        principalTable: "TransportGroups",
                        principalColumn: "TransportGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transports_TransportGroupId",
                table: "Transports",
                column: "TransportGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "TransportGroups");
        }
    }
}
