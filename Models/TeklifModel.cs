using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitirmeProjesiErp.Models
{
    public class Teklif
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string _key { get; set; }
        //public string KalemID { get; set; }a
        public string _key_prj_proje { get; set; }//public virtual _key_prj_proje _key_prj_proje { get; set; }
        [ForeignKey("CariKart")]
        public string _key_scf_carikart { get; set; }//public virtual _key_scf_carikart _key_scf_carikart { get; set; }
        [ForeignKey("SatisElemanlari")]
        public string _key_satiselemanlari { get; set; }
        [ForeignKey("CariKartAdresler")]
        public string _key_scf_carikart_adresleri { get; set; }//public virtual ICollection<_key_scf_carikart_adresleri> _key_scf_carikart_adresleris { get; set; }
        [ForeignKey("CariKartYetkili")]
        public string _key_scf_carikart_yetkili { get; set; }//public virtual _key_scf_carikart_yetkili _key_scf_carikart_yetkili { get; set; }
        [ForeignKey("OdemePlani")]
        public string _key_scf_odeme_plani { get; set; }//public virtual _key_scf_odeme_plani _key_scf_odeme_plani { get; set; }
        [ForeignKey("Doviz")]
        public string _key_sis_doviz { get; set; }//public virtual _key_sis_doviz _key_sis_doviz { get; set; }
        public string _key_sis_doviz_raporlama { get; set; }//public virtual _key_sis_doviz_raporlama _key_sis_doviz_raporlama { get; set; }
        public string _key_sis_sube_source { get; set; }//public virtual _key_sis_sube_source _key_sis_sube_source { get; set; }
        public string aciklama { get; set; }
        public string aciklama1 { get; set; }
        public string aciklama2 { get; set; }
        public string aciklama3 { get; set; }
        public string bagkur { get; set; }
        public string bagkurdvz { get; set; }
        public string bagkuryuzde { get; set; }
        public string borsa { get; set; }
        public string borsadvz { get; set; }
        public string fisno { get; set; }
        public string belgeno { get; set; }
        public string dovizkuru { get; set; }
        public string dovizturu { get; set; }
        public string ekalan1 { get; set; }
        public string ekalan2 { get; set; }
        public string ekalan3 { get; set; }
        public string ekalan4 { get; set; }
        public string ekalan5 { get; set; }
        public string ekalan6 { get; set; }
        public string raporlamadovizkuru { get; set; }
        public string saat { get; set; }
        public string tarih { get; set; }
        public string teslimattarihi { get; set; }
        public string tipi { get; set; }
        public string sevkadresi1 { get; set; }
        public string sevkadresi2 { get; set; }
        public string sevkadresi3 { get; set; }
        public string toplam { get; set; }
        public string toplamdvz { get; set; }
        public string toplamara { get; set; }
        public string toplamaradvz { get; set; }
        public string toplamindirim { get; set; }
        public string toplamindirimdvz { get; set; }
        public string toplamkdv { get; set; }
        public string toplamkdvdvz { get; set; }
        public string toplamkdvtevkifati { get; set; }
        public string toplamkdvtevkifatidvz { get; set; }
        public string toplammasraf { get; set; }
        public string toplammasrafdvz { get; set; }
        public string toplamov { get; set; }
        public string toplamovdvz { get; set; }
        public string turu { get; set; } // 1 verile---2 alınan
        public string onay { get; set; }// Teklif/analiz/kabul/ret
        public string odemeplaniaciklama { get; set; }
        public string odemeplanikodu { get; set; }
        public virtual ICollection<TeklifKalemi> TeklifKalemis { get; set; }
        public virtual ICollection<SatisElemanlari> SatisElemanlaris { get; set; }
        public virtual ICollection<OdemePlani> OdemePlanis { get; set; }
        public CariKart CariKart { get; set; }
        public string aktarildi { get; set; } = "0";
        public virtual Rpr_dinamik_raporparametreleri_getir rpr_dinamik_raporparametreleri_getirs { get; set; }
        public string diakey { get; set; }
        public string diafisno { get; set; }
        public string _key_rpr_dinamik_raporparametreleri_getirs { get; set; }






    }
    public class CariKartYetkili
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string _key { get; set; }
        public string _key_scf_carikart { get; set; }
        public string _key_sis_rehber_karti { get; set; }
        public string adsoyad { get; set; } = "1";
        public string carikartkodu { get; set; }
        public string kodu { get; set; }
        public string ceptelno { get; set; }
        public string istelno { get; set; }
        public string gorev { get; set; }
    }
    public class Doviz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string _key { get; set; }
        public string adi { get; set; }
        public string uzunadi { get; set; }
        public string aktif { get; set; } = "1";

    }
    public class Rpr_dinamik_raporparametreleri_getir
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string _key { get; set; }
        public string isim { get; set; }

    }
    public class CariKartAdresler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string _key { get; set; }

        [ForeignKey("CariKart")]
        public string _key_scf_carikart { get; set; }
        public string adresadi { get; set; }
        public string carikartunvani { get; set; }
        public string adres1 { get; set; }
        public string adres2 { get; set; }

    }
    public class SatisElemanlari
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string _key { get; set; }

        [ForeignKey("CariKart")]
        public string _key_scf_carikart { get; set; }
        public string aciklama { get; set; }
        public string ceptel { get; set; }
        public string durum { get; set; }
        public string kodu { get; set; }

    }

    public class TeklifKalemi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[DatabaseGenerated(DatabaseGeneratedOption.)]
        public string _key { get; set; }
        [ForeignKey("StokKart")]
        public string _key_kalemturu { get; set; }
        public string _key_sis_sube_source { get; set; }
        public string _key_prj_proje { get; set; }
        public string _key_scf_fiyatkart { get; set; }
        public string _key_scf_odeme_plani { get; set; }
        public string _key_scf_teklif { get; set; }
        [ForeignKey("Doviz")]
        public string _key_sis_doviz { get; set; }

        [ForeignKey("SatisElemanlari")]
        public string _key_scf_satiselemani { get; set; }
        public string birimadi { get; set; }
        public string birimfiyati { get; set; }
        public string dovizadi { get; set; }
        public string dovizkuru { get; set; }
        public string miktar { get; set; }
        public string note { get; set; }
        public string note2 { get; set; }
        public string tutari { get; set; }
        [NotMapped]
        public /*List<CariKart>*/ IEnumerable<SelectListItem> StokKarts { get; set; }
        public StokKart StokKart { get; set; }
        public Teklif teklif { get; set; }

        
    }
    public class StokKart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string _key { get; set; }
        public string stokkartkodu { get; set; }
        public string aciklama { get; set; }
        public string aktif { get; set; } = "1";
        public string birimisimleri { get; set; }
        public string birimkeyleri { get; set; }
    }

    
    public class Varyant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string _key { get; set; }
    }
    public class CariKart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string _key { get; set; }
        [ConcurrencyCheck]
        public string unvan { get; set; }
        public string Aktif { get; set; } = "1";
    }
    public class OdemePlani
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string _key { get; set; }
        [ConcurrencyCheck]
        public string Kodu { get; set; }
        public string Aciklama { get; set; }
        public string Aktif { get; set; } = "1";


    }
    public class Depo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string _key { get; set; }
        public string depokodu { get; set; }

    }
    public class Sube
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string _key { get; set; }
        public string subekodu { get; set; }
        public string subeadi { get; set; }

    }
}
