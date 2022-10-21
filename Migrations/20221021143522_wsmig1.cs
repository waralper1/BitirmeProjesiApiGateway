using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitirmeProjesiErp.Migrations
{
    public partial class wsmig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebServisBilgi",
                columns: table => new
                {
                    WSID = table.Column<int>(type: "int", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SunucuAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sube = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Depo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirmaKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonemKod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebServisBilgi", x => x.WSID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebServisBilgi");
        }
    }
}
