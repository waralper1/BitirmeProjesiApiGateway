using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitirmeProjesiErp.Migrations.Fiyatlar
{
    public partial class fiy1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fiyat1",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genislik = table.Column<int>(type: "int", nullable: false),
                    uzunluk = table.Column<int>(type: "int", nullable: false),
                    fiyat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fiyat1", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "fiyat2",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genislik = table.Column<int>(type: "int", nullable: false),
                    uzunluk = table.Column<int>(type: "int", nullable: false),
                    fiyat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fiyat2", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "fiyat3",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genislik = table.Column<int>(type: "int", nullable: false),
                    uzunluk = table.Column<int>(type: "int", nullable: false),
                    fiyat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fiyat3", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "fiyat4",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genislik = table.Column<int>(type: "int", nullable: false),
                    uzunluk = table.Column<int>(type: "int", nullable: false),
                    fiyat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fiyat4", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "fiyat5",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genislik = table.Column<int>(type: "int", nullable: false),
                    uzunluk = table.Column<int>(type: "int", nullable: false),
                    fiyat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fiyat5", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "fiyat6",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genislik = table.Column<int>(type: "int", nullable: false),
                    uzunluk = table.Column<int>(type: "int", nullable: false),
                    fiyat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fiyat6", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "yedek1",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genislik = table.Column<int>(type: "int", nullable: false),
                    uzunluk = table.Column<int>(type: "int", nullable: false),
                    fiyat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_yedek1", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "yedek2",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genislik = table.Column<int>(type: "int", nullable: false),
                    uzunluk = table.Column<int>(type: "int", nullable: false),
                    fiyat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_yedek2", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "yedek3",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genislik = table.Column<int>(type: "int", nullable: false),
                    uzunluk = table.Column<int>(type: "int", nullable: false),
                    fiyat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_yedek3", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "yedek4",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genislik = table.Column<int>(type: "int", nullable: false),
                    uzunluk = table.Column<int>(type: "int", nullable: false),
                    fiyat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_yedek4", x => x._key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fiyat1");

            migrationBuilder.DropTable(
                name: "fiyat2");

            migrationBuilder.DropTable(
                name: "fiyat3");

            migrationBuilder.DropTable(
                name: "fiyat4");

            migrationBuilder.DropTable(
                name: "fiyat5");

            migrationBuilder.DropTable(
                name: "fiyat6");

            migrationBuilder.DropTable(
                name: "yedek1");

            migrationBuilder.DropTable(
                name: "yedek2");

            migrationBuilder.DropTable(
                name: "yedek3");

            migrationBuilder.DropTable(
                name: "yedek4");
        }
    }
}
