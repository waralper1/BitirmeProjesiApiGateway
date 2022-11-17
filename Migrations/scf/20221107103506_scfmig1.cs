using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitirmeProjesiErp.Migrations.scf
{
    public partial class scfmig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CariKarts",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    unvan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aktif = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CariKarts", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "Rpr_dinamik_raporparametreleri_getir",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isim = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rpr_dinamik_raporparametreleri_getir", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "StokKarts",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    stokkartkodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aktif = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birimisimleri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birimkeyleri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokKarts", x => x._key);
                });

            migrationBuilder.CreateTable(
                name: "Teklifs",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    _key_prj_proje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_scf_carikart = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    _key_satiselemanlari = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_scf_carikart_adresleri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_scf_carikart_yetkili = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_scf_odeme_plani = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_sis_doviz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_sis_doviz_raporlama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_sis_sube_source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aciklama1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aciklama2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aciklama3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bagkur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bagkurdvz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bagkuryuzde = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    borsa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    borsadvz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fisno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    belgeno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dovizkuru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dovizturu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ekalan1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ekalan2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ekalan3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ekalan4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ekalan5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ekalan6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    raporlamadovizkuru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    saat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tarih = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    teslimattarihi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sevkadresi1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sevkadresi2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sevkadresi3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplamdvz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplamara = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplamaradvz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplamindirim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplamindirimdvz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplamkdv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplamkdvdvz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplamkdvtevkifati = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplamkdvtevkifatidvz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplammasraf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplammasrafdvz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplamov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toplamovdvz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    turu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    onay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    odemeplaniaciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    odemeplanikodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aktarildi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rpr_dinamik_raporparametreleri_getirs_key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    diakey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    diafisno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_rpr_dinamik_raporparametreleri_getirs = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teklifs", x => x._key);
                    table.ForeignKey(
                        name: "FK_Teklifs_CariKarts__key_scf_carikart",
                        column: x => x._key_scf_carikart,
                        principalTable: "CariKarts",
                        principalColumn: "_key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teklifs_Rpr_dinamik_raporparametreleri_getir_rpr_dinamik_raporparametreleri_getirs_key",
                        column: x => x.rpr_dinamik_raporparametreleri_getirs_key,
                        principalTable: "Rpr_dinamik_raporparametreleri_getir",
                        principalColumn: "_key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OdemePlani",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Kodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aktif = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teklif_key = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdemePlani", x => x._key);
                    table.ForeignKey(
                        name: "FK_OdemePlani_Teklifs_Teklif_key",
                        column: x => x.Teklif_key,
                        principalTable: "Teklifs",
                        principalColumn: "_key");
                });

            migrationBuilder.CreateTable(
                name: "SatisElemanlari",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    _key_scf_carikart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ceptel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    durum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teklif_key = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatisElemanlari", x => x._key);
                    table.ForeignKey(
                        name: "FK_SatisElemanlari_Teklifs_Teklif_key",
                        column: x => x.Teklif_key,
                        principalTable: "Teklifs",
                        principalColumn: "_key");
                });

            migrationBuilder.CreateTable(
                name: "TeklifKalemis",
                columns: table => new
                {
                    _key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    _key_kalemturu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    _key_sis_sube_source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_prj_proje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_scf_fiyatkart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_scf_odeme_plani = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_scf_teklif = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_sis_doviz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _key_scf_satiselemani = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birimadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birimfiyati = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dovizadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dovizkuru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    miktar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    note2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tutari = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    teklif_key = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeklifKalemis", x => x._key);
                    table.ForeignKey(
                        name: "FK_TeklifKalemis_StokKarts__key_kalemturu",
                        column: x => x._key_kalemturu,
                        principalTable: "StokKarts",
                        principalColumn: "_key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeklifKalemis_Teklifs_teklif_key",
                        column: x => x.teklif_key,
                        principalTable: "Teklifs",
                        principalColumn: "_key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OdemePlani_Teklif_key",
                table: "OdemePlani",
                column: "Teklif_key");

            migrationBuilder.CreateIndex(
                name: "IX_SatisElemanlari_Teklif_key",
                table: "SatisElemanlari",
                column: "Teklif_key");

            migrationBuilder.CreateIndex(
                name: "IX_TeklifKalemis__key_kalemturu",
                table: "TeklifKalemis",
                column: "_key_kalemturu");

            migrationBuilder.CreateIndex(
                name: "IX_TeklifKalemis_teklif_key",
                table: "TeklifKalemis",
                column: "teklif_key");

            migrationBuilder.CreateIndex(
                name: "IX_Teklifs__key_scf_carikart",
                table: "Teklifs",
                column: "_key_scf_carikart");

            migrationBuilder.CreateIndex(
                name: "IX_Teklifs_rpr_dinamik_raporparametreleri_getirs_key",
                table: "Teklifs",
                column: "rpr_dinamik_raporparametreleri_getirs_key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OdemePlani");

            migrationBuilder.DropTable(
                name: "SatisElemanlari");

            migrationBuilder.DropTable(
                name: "TeklifKalemis");

            migrationBuilder.DropTable(
                name: "StokKarts");

            migrationBuilder.DropTable(
                name: "Teklifs");

            migrationBuilder.DropTable(
                name: "CariKarts");

            migrationBuilder.DropTable(
                name: "Rpr_dinamik_raporparametreleri_getir");
        }
    }
}
