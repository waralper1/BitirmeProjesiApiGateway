﻿// <auto-generated />
using BitirmeProjesiErp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BitirmeProjesiErp.Migrations.scf
{
    [DbContext(typeof(scfContext))]
    [Migration("20221212210111_scfmig1")]
    partial class scfmig1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BitirmeProjesiErp.Models.CariKart", b =>
                {
                    b.Property<string>("_key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Aktif")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("unvan")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("_key");

                    b.ToTable("CariKarts");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.CariKartAdresler", b =>
                {
                    b.Property<string>("_key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("_key_scf_carikart")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adres1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adres2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adresadi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("carikartunvani")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("_key");

                    b.ToTable("CariKartAdreslers");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.CariKartYetkili", b =>
                {
                    b.Property<string>("_key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("_key_scf_carikart")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_sis_rehber_karti")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adsoyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("carikartkodu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ceptelno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gorev")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("istelno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("kodu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("_key");

                    b.ToTable("CariKartYetkilis");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.Doviz", b =>
                {
                    b.Property<string>("_key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("adi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aktif")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("uzunadi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("_key");

                    b.ToTable("Dovizs");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.OdemePlani", b =>
                {
                    b.Property<string>("_key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Aktif")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kodu")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Teklif_key")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("_key");

                    b.HasIndex("Teklif_key");

                    b.ToTable("OdemePlanis");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.Rpr_dinamik_raporparametreleri_getir", b =>
                {
                    b.Property<string>("_key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("isim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("_key");

                    b.ToTable("rpr_dinamik_raporparametreleri_getir");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.SatisElemanlari", b =>
                {
                    b.Property<string>("_key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Teklif_key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("_key_scf_carikart")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aciklama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ceptel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("durum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("kodu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("_key");

                    b.HasIndex("Teklif_key");

                    b.ToTable("SatisElemanlaris");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.StokKart", b =>
                {
                    b.Property<string>("_key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("aciklama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aktif")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("birimisimleri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("birimkeyleri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("stokkartkodu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("_key");

                    b.ToTable("StokKarts");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.Teklif", b =>
                {
                    b.Property<string>("_key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("_key_prj_proje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_rpr_dinamik_raporparametreleri_getirs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_satiselemanlari")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_scf_carikart")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("_key_scf_carikart_adresleri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_scf_carikart_yetkili")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_scf_odeme_plani")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_sis_doviz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_sis_doviz_raporlama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_sis_sube_source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aciklama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aciklama1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aciklama2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aciklama3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aktarildi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bagkur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bagkurdvz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bagkuryuzde")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("belgeno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("borsa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("borsadvz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("diafisno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("diakey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dinamik10")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dinamik7")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dinamik8")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dovizkuru")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dovizturu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ekalan1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ekalan2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ekalan3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ekalan4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ekalan5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ekalan6")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fisno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("odemeplaniaciklama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("odemeplanikodu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("onay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("raporlamadovizkuru")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rpr_dinamik_raporparametreleri_getirs_key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("saat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sevkadresi1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sevkadresi2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sevkadresi3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tarih")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("teslimattarihi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplamara")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplamaradvz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplamdvz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplamindirim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplamindirimdvz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplamkdv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplamkdvdvz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplamkdvtevkifati")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplamkdvtevkifatidvz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplammasraf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplammasrafdvz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplamov")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toplamovdvz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("turu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("_key");

                    b.HasIndex("_key_scf_carikart");

                    b.HasIndex("rpr_dinamik_raporparametreleri_getirs_key");

                    b.ToTable("Teklifs");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.TeklifKalemi", b =>
                {
                    b.Property<string>("_key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("_key_kalemturu")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("_key_prj_proje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_scf_fiyatkart")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_scf_odeme_plani")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_scf_satiselemani")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_scf_teklif")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_sis_doviz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_key_sis_sube_source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("birimadi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("birimfiyati")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dovizadi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dovizkuru")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("miktar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("note2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("teklif_key")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("tutari")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("_key");

                    b.HasIndex("_key_kalemturu");

                    b.HasIndex("teklif_key");

                    b.ToTable("TeklifKalemis");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.TeklifViewModel", b =>
                {
                    b.Property<string>("CariKart_key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("carikey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("teklif_key")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("CariKart_key");

                    b.HasIndex("teklif_key");

                    b.ToTable("TeklifViewModel");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.OdemePlani", b =>
                {
                    b.HasOne("BitirmeProjesiErp.Models.Teklif", null)
                        .WithMany("OdemePlanis")
                        .HasForeignKey("Teklif_key");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.SatisElemanlari", b =>
                {
                    b.HasOne("BitirmeProjesiErp.Models.Teklif", null)
                        .WithMany("SatisElemanlaris")
                        .HasForeignKey("Teklif_key");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.Teklif", b =>
                {
                    b.HasOne("BitirmeProjesiErp.Models.CariKart", "CariKart")
                        .WithMany()
                        .HasForeignKey("_key_scf_carikart")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BitirmeProjesiErp.Models.Rpr_dinamik_raporparametreleri_getir", "rpr_dinamik_raporparametreleri_getirs")
                        .WithMany()
                        .HasForeignKey("rpr_dinamik_raporparametreleri_getirs_key");

                    b.Navigation("CariKart");

                    b.Navigation("rpr_dinamik_raporparametreleri_getirs");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.TeklifKalemi", b =>
                {
                    b.HasOne("BitirmeProjesiErp.Models.StokKart", "StokKart")
                        .WithMany()
                        .HasForeignKey("_key_kalemturu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BitirmeProjesiErp.Models.Teklif", "teklif")
                        .WithMany("TeklifKalemis")
                        .HasForeignKey("teklif_key")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StokKart");

                    b.Navigation("teklif");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.TeklifViewModel", b =>
                {
                    b.HasOne("BitirmeProjesiErp.Models.CariKart", "CariKart")
                        .WithMany()
                        .HasForeignKey("CariKart_key");

                    b.HasOne("BitirmeProjesiErp.Models.Teklif", "teklif")
                        .WithMany()
                        .HasForeignKey("teklif_key")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CariKart");

                    b.Navigation("teklif");
                });

            modelBuilder.Entity("BitirmeProjesiErp.Models.Teklif", b =>
                {
                    b.Navigation("OdemePlanis");

                    b.Navigation("SatisElemanlaris");

                    b.Navigation("TeklifKalemis");
                });
#pragma warning restore 612, 618
        }
    }
}