using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitirmeProjesiErp.Migrations.scf
{
    public partial class scfmig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OdemePlani_Teklifs_Teklif_key",
                table: "OdemePlani");

            migrationBuilder.DropForeignKey(
                name: "FK_SatisElemanlari_Teklifs_Teklif_key",
                table: "SatisElemanlari");

            migrationBuilder.DropForeignKey(
                name: "FK_Teklifs_Rpr_dinamik_raporparametreleri_getir_rpr_dinamik_raporparametreleri_getirs_key",
                table: "Teklifs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SatisElemanlari",
                table: "SatisElemanlari");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OdemePlani",
                table: "OdemePlani");

            migrationBuilder.RenameTable(
                name: "SatisElemanlari",
                newName: "SatisElemanlaris");

            migrationBuilder.RenameTable(
                name: "OdemePlani",
                newName: "OdemePlanis");

            migrationBuilder.RenameIndex(
                name: "IX_SatisElemanlari_Teklif_key",
                table: "SatisElemanlaris",
                newName: "IX_SatisElemanlaris_Teklif_key");

            migrationBuilder.RenameIndex(
                name: "IX_OdemePlani_Teklif_key",
                table: "OdemePlanis",
                newName: "IX_OdemePlanis_Teklif_key");

            migrationBuilder.AlterColumn<string>(
                name: "rpr_dinamik_raporparametreleri_getirs_key",
                table: "Teklifs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SatisElemanlaris",
                table: "SatisElemanlaris",
                column: "_key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OdemePlanis",
                table: "OdemePlanis",
                column: "_key");

            migrationBuilder.CreateTable(
                name: "CariKartAdreslers",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    _key_scf_carikart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adresadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carikartunvani = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adres1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adres2 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CariKartAdreslers", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "CariKartYetkilis",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    _key_scf_carikart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_sis_rehber_karti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adsoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carikartkodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ceptelno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    istelno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gorev = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CariKartYetkilis", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "Depos",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    depokodu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depos", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "Dovizs",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uzunadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aktif = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dovizs", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "Subes",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    subekodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    subeadi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subes", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "TeklifViewModel",
                columns: table => new
                {
                    teklif_key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    carikey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CariKart_key = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TeklifViewModel_CariKarts_CariKart_key",
                        column: x => x.CariKart_key,
                        principalTable: "CariKarts",
                        principalColumn: "_key");
                    table.ForeignKey(
                        name: "FK_TeklifViewModel_Teklifs_teklif_key",
                        column: x => x.teklif_key,
                        principalTable: "Teklifs",
                        principalColumn: "_key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Varyants",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Varyants", x => x._key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeklifViewModel_CariKart_key",
                table: "TeklifViewModel",
                column: "CariKart_key");

            migrationBuilder.CreateIndex(
                name: "IX_TeklifViewModel_teklif_key",
                table: "TeklifViewModel",
                column: "teklif_key");

            migrationBuilder.AddForeignKey(
                name: "FK_OdemePlanis_Teklifs_Teklif_key",
                table: "OdemePlanis",
                column: "Teklif_key",
                principalTable: "Teklifs",
                principalColumn: "_key");

            migrationBuilder.AddForeignKey(
                name: "FK_SatisElemanlaris_Teklifs_Teklif_key",
                table: "SatisElemanlaris",
                column: "Teklif_key",
                principalTable: "Teklifs",
                principalColumn: "_key");

            migrationBuilder.AddForeignKey(
                name: "FK_Teklifs_Rpr_dinamik_raporparametreleri_getir_rpr_dinamik_raporparametreleri_getirs_key",
                table: "Teklifs",
                column: "rpr_dinamik_raporparametreleri_getirs_key",
                principalTable: "Rpr_dinamik_raporparametreleri_getir",
                principalColumn: "_key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OdemePlanis_Teklifs_Teklif_key",
                table: "OdemePlanis");

            migrationBuilder.DropForeignKey(
                name: "FK_SatisElemanlaris_Teklifs_Teklif_key",
                table: "SatisElemanlaris");

            migrationBuilder.DropForeignKey(
                name: "FK_Teklifs_Rpr_dinamik_raporparametreleri_getir_rpr_dinamik_raporparametreleri_getirs_key",
                table: "Teklifs");

            migrationBuilder.DropTable(
                name: "CariKartAdreslers");

            migrationBuilder.DropTable(
                name: "CariKartYetkilis");

            migrationBuilder.DropTable(
                name: "Depos");

            migrationBuilder.DropTable(
                name: "Dovizs");

            migrationBuilder.DropTable(
                name: "Subes");

            migrationBuilder.DropTable(
                name: "TeklifViewModel");

            migrationBuilder.DropTable(
                name: "Varyants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SatisElemanlaris",
                table: "SatisElemanlaris");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OdemePlanis",
                table: "OdemePlanis");

            migrationBuilder.RenameTable(
                name: "SatisElemanlaris",
                newName: "SatisElemanlari");

            migrationBuilder.RenameTable(
                name: "OdemePlanis",
                newName: "OdemePlani");

            migrationBuilder.RenameIndex(
                name: "IX_SatisElemanlaris_Teklif_key",
                table: "SatisElemanlari",
                newName: "IX_SatisElemanlari_Teklif_key");

            migrationBuilder.RenameIndex(
                name: "IX_OdemePlanis_Teklif_key",
                table: "OdemePlani",
                newName: "IX_OdemePlani_Teklif_key");

            migrationBuilder.AlterColumn<string>(
                name: "rpr_dinamik_raporparametreleri_getirs_key",
                table: "Teklifs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SatisElemanlari",
                table: "SatisElemanlari",
                column: "_key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OdemePlani",
                table: "OdemePlani",
                column: "_key");

            migrationBuilder.AddForeignKey(
                name: "FK_OdemePlani_Teklifs_Teklif_key",
                table: "OdemePlani",
                column: "Teklif_key",
                principalTable: "Teklifs",
                principalColumn: "_key");

            migrationBuilder.AddForeignKey(
                name: "FK_SatisElemanlari_Teklifs_Teklif_key",
                table: "SatisElemanlari",
                column: "Teklif_key",
                principalTable: "Teklifs",
                principalColumn: "_key");

            migrationBuilder.AddForeignKey(
                name: "FK_Teklifs_Rpr_dinamik_raporparametreleri_getir_rpr_dinamik_raporparametreleri_getirs_key",
                table: "Teklifs",
                column: "rpr_dinamik_raporparametreleri_getirs_key",
                principalTable: "Rpr_dinamik_raporparametreleri_getir",
                principalColumn: "_key",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
